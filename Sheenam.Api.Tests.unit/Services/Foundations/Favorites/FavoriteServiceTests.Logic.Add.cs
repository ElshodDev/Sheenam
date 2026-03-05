//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Favorites;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Favorites
{
    public partial class FavoriteServiceTests
    {
        [Fact]
        public async Task ShouldAddFavoriteAsync()
        {
            // given
            Favorite randomFavorite = CreateRandomFavorite();
            Favorite inputFavorite = randomFavorite;
            Favorite storageFavorite = inputFavorite;
            Favorite expectedFavorite = storageFavorite.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertFavoriteAsync(inputFavorite))
                    .ReturnsAsync(storageFavorite);

            // when
            Favorite actualFavorite =
                await this.favoriteService.AddFavoriteAsync(inputFavorite);

            // then
            actualFavorite.Should().BeEquivalentTo(expectedFavorite);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertFavoriteAsync(inputFavorite),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveFavoriteByIdAsync()
        {
            // given
            Guid randomFavoriteId = Guid.NewGuid();
            Favorite randomFavorite = CreateRandomFavorite();
            Favorite storageFavorite = randomFavorite;
            Favorite expectedFavorite = storageFavorite.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectFavoriteByIdAsync(randomFavoriteId))
                    .ReturnsAsync(storageFavorite);

            // when
            Favorite actualFavorite =
                await this.favoriteService.RetrieveFavoriteByIdAsync(randomFavoriteId);

            // then
            actualFavorite.Should().BeEquivalentTo(expectedFavorite);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectFavoriteByIdAsync(randomFavoriteId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveFavoriteByIdAsync()
        {
            // given
            Guid randomFavoriteId = Guid.NewGuid();
            Favorite randomFavorite = CreateRandomFavorite();
            Favorite storageFavorite = randomFavorite;
            Favorite expectedFavorite = storageFavorite.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectFavoriteByIdAsync(randomFavoriteId))
                    .ReturnsAsync(storageFavorite);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteFavoriteAsync(storageFavorite))
                    .ReturnsAsync(expectedFavorite);

            // when
            Favorite actualFavorite =
                await this.favoriteService.RemoveFavoriteByIdAsync(randomFavoriteId);

            // then
            actualFavorite.Should().BeEquivalentTo(expectedFavorite);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectFavoriteByIdAsync(randomFavoriteId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteFavoriteAsync(storageFavorite),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}