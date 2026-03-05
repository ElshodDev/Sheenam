//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.Favorites;
using Sheenam.Api.Models.Foundations.Favorites.Exceptions;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Favorites
{
    public partial class FavoriteServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Favorite someFavorite = CreateRandomFavorite();
            SqlException sqlException = GetSqlError();

            var failedFavoriteStorageException =
                new FailedFavoriteStorageException(sqlException);

            var expectedFavoriteDependencyException =
                new FavoriteDependencyException(failedFavoriteStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertFavoriteAsync(someFavorite))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Favorite> addFavoriteTask =
                this.favoriteService.AddFavoriteAsync(someFavorite);

            FavoriteDependencyException actualFavoriteDependencyException =
                await Assert.ThrowsAsync<FavoriteDependencyException>(() =>
                    addFavoriteTask.AsTask());

            // then
            actualFavoriteDependencyException.Should().BeEquivalentTo(
                expectedFavoriteDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertFavoriteAsync(someFavorite),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedFavoriteDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfUnexpectedErrorOccursAndLogItAsync()
        {
            // given
            Favorite someFavorite = CreateRandomFavorite();
            var serviceException = new Exception();

            var failedFavoriteServiceException =
                new FailedFavoriteServiceException(serviceException);

            var expectedFavoriteServiceException =
                new FavoriteServiceException(failedFavoriteServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertFavoriteAsync(someFavorite))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Favorite> addFavoriteTask =
                this.favoriteService.AddFavoriteAsync(someFavorite);

            FavoriteServiceException actualFavoriteServiceException =
                await Assert.ThrowsAsync<FavoriteServiceException>(() =>
                    addFavoriteTask.AsTask());

            // then
            actualFavoriteServiceException.Should().BeEquivalentTo(
                expectedFavoriteServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertFavoriteAsync(someFavorite),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedFavoriteServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}