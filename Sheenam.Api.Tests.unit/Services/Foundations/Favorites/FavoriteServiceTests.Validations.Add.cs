//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.Favorites;
using Sheenam.Api.Models.Foundations.Favorites.Exceptions;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Favorites
{
    public partial class FavoriteServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfFavoriteIsNullAndLogItAsync()
        {
            // given
            Favorite nullFavorite = null;
            var nullFavoriteException = new NullFavoriteException();

            var expectedFavoriteValidationException =
                new FavoriteValidationException(nullFavoriteException);

            // when
            ValueTask<Favorite> addFavoriteTask =
                this.favoriteService.AddFavoriteAsync(nullFavorite);

            // then
            await Assert.ThrowsAsync<FavoriteValidationException>(() =>
                addFavoriteTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedFavoriteValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertFavoriteAsync(It.IsAny<Favorite>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfFavoriteIsInvalidAndLogItAsync()
        {
            // given
            var invalidFavorite = new Favorite
            {
                Id = Guid.Empty,
                UserId = Guid.Empty,
                CreatedDate = default
            };

            var invalidFavoriteException = new InvalidFavoriteException();

            invalidFavoriteException.AddData(
                key: nameof(Favorite.Id),
                values: "Id is required");

            invalidFavoriteException.AddData(
                key: nameof(Favorite.UserId),
                values: "Id is required");

            invalidFavoriteException.AddData(
                key: nameof(Favorite.CreatedDate),
                values: "Date is required");

            var expectedFavoriteValidationException =
                new FavoriteValidationException(invalidFavoriteException);

            // when
            ValueTask<Favorite> addFavoriteTask =
                this.favoriteService.AddFavoriteAsync(invalidFavorite);

            // then
            await Assert.ThrowsAsync<FavoriteValidationException>(() =>
                addFavoriteTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedFavoriteValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertFavoriteAsync(It.IsAny<Favorite>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidFavoriteId = Guid.Empty;
            var invalidFavoriteException = new InvalidFavoriteException();

            invalidFavoriteException.AddData(
                key: nameof(Favorite.Id),
                values: "Id is required");

            var expectedFavoriteValidationException =
                new FavoriteValidationException(invalidFavoriteException);

            // when
            ValueTask<Favorite> retrieveFavoriteTask =
                this.favoriteService.RetrieveFavoriteByIdAsync(invalidFavoriteId);

            // then
            await Assert.ThrowsAsync<FavoriteValidationException>(() =>
                retrieveFavoriteTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedFavoriteValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectFavoriteByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfNotFoundAndLogItAsync()
        {
            // given
            Guid randomFavoriteId = Guid.NewGuid();
            Favorite noFavorite = null;

            var notFoundFavoriteException =
                new NotFoundFavoriteException(randomFavoriteId);

            var expectedFavoriteValidationException =
                new FavoriteValidationException(notFoundFavoriteException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectFavoriteByIdAsync(randomFavoriteId))
                    .ReturnsAsync(noFavorite);

            // when
            ValueTask<Favorite> retrieveFavoriteTask =
                this.favoriteService.RetrieveFavoriteByIdAsync(randomFavoriteId);

            // then
            await Assert.ThrowsAsync<FavoriteValidationException>(() =>
                retrieveFavoriteTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectFavoriteByIdAsync(randomFavoriteId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedFavoriteValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}