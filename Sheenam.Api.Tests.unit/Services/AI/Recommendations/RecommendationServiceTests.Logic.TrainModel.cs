//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Microsoft.ML;
using Moq;
using Sheenam.Api.Models.AI.Recommendations;
using Sheenam.Api.Models.Foundations.Homes;

namespace Sheenam.Api.Tests.Unit.Services.AI.Recommendations
{
    public partial class RecommendationServiceTests
    {
        [Fact]
        public async Task ShouldTrainModelAsync()
        {
            // given
            IQueryable<Home> randomHomes = CreateRandomHomes();
            ITransformer mockModel = null;
            List<HomeFeatureData> capturedTrainingData = null;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Returns(randomHomes);

            this.mlBrokerMock.Setup(broker =>
                broker.TrainRecommendationModel(It.IsAny<List<HomeFeatureData>>()))
                    .Callback<List<HomeFeatureData>>(data => capturedTrainingData = data)
                    .Returns(mockModel);

            this.mlBrokerMock.Setup(broker =>
                broker.SaveModel(It.IsAny<ITransformer>(), It.IsAny<string>()));

            // when
            await this.recommendationService.TrainModelAsync();

            // then
            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomes(),
                    Times.Once);

            this.mlBrokerMock.Verify(broker =>
                broker.TrainRecommendationModel(It.IsAny<List<HomeFeatureData>>()),
                    Times.Once);

            this.mlBrokerMock.Verify(broker =>
                broker.SaveModel(It.IsAny<ITransformer>(), It.IsAny<string>()),
                    Times.Once);

            capturedTrainingData.Should().NotBeNull();
            capturedTrainingData.Should().HaveCount(randomHomes.Count());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.mlBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldGenerateCorrectTrainingDataAsync()
        {
            // given
            var testHome = new Home
            {
                Price = 1000,
                NumberOfBedrooms = 3,
                NumberOfBathrooms = 2,
                Area = 80,
                IsPetAllowed = true,
                IsShared = false
            };

            var homes = new List<Home> { testHome }.AsQueryable();
            List<HomeFeatureData> capturedData = null;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Returns(homes);

            this.mlBrokerMock.Setup(broker =>
                broker.TrainRecommendationModel(It.IsAny<List<HomeFeatureData>>()))
                    .Callback<List<HomeFeatureData>>(data => capturedData = data)
                    .Returns((ITransformer)null);

            this.mlBrokerMock.Setup(broker =>
                broker.SaveModel(It.IsAny<ITransformer>(), It.IsAny<string>()));

            // when
            await this.recommendationService.TrainModelAsync();

            // then
            capturedData.Should().NotBeNull();
            capturedData.Should().HaveCount(1);

            var featureData = capturedData.First();
            featureData.Price.Should().Be(1000);
            featureData.Bedrooms.Should().Be(3);
            featureData.Bathrooms.Should().Be(2);
            featureData.Area.Should().Be(80);
            featureData.IsPetAllowed.Should().Be(1f);
            featureData.IsShared.Should().Be(0f);
        }

      

        [Fact]
        public async Task ShouldIgnoreNonVacantHomesAsync()
        {
            // given
            var homes = new List<Home>
            {
                new Home { Id = Guid.NewGuid(), IsVacant = true, Price = 1200, NumberOfBedrooms = 2, IsPetAllowed = true },
                new Home { Id = Guid.NewGuid(), IsVacant = false, Price = 900, NumberOfBedrooms = 3, IsPetAllowed = true },
                new Home { Id = Guid.NewGuid(), IsVacant = true, Price = 1400, NumberOfBedrooms = 2, IsPetAllowed = false }
            }.AsQueryable();

            var preferences = new GuestPreferences
            {
                MaxPrice = 1500,
                MinBedrooms = 2,
                NeedsPetAllowed = false
            };

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Returns(homes);

            // when
            List<HomeRecommendation> actualRecommendations =
                await this.recommendationService.GetRecommendedHomesAsync(preferences);

            // then
            actualRecommendations.Should().NotBeNull();
            actualRecommendations.All(r => r.Price <= 1500).Should().BeTrue();
            actualRecommendations.All(r => r.NumberOfBedrooms >= 2).Should().BeTrue();
            actualRecommendations.All(r =>
                homes.First(h => h.Id == r.HomeId).IsVacant).Should().BeTrue();
        }

        [Fact]
        public async Task ShouldCalculateMatchScoreCorrectlyAsync()
        {
            // given
            var home = new Home
            {
                Id = Guid.NewGuid(),
                IsVacant = true,
                Price = 900, 
                NumberOfBedrooms = 3,
                NumberOfBathrooms = 2,
                Area = 80,
                IsPetAllowed = true,
                IsShared = false,
                Type = HouseType.Flat
            };

            var preferences = new GuestPreferences
            {
                MaxPrice = 1200,
                MinBedrooms = 2,
                NeedsPetAllowed = true,
                PreferPrivate = true,
                PreferredType = "Flat"
            };

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Returns(new List<Home> { home }.AsQueryable());

            // when
            List<HomeRecommendation> actualRecommendations =
                await this.recommendationService.GetRecommendedHomesAsync(preferences);

            // then
            actualRecommendations.Should().HaveCount(1);
            var rec = actualRecommendations.First();
            rec.MatchScore.Should().BeGreaterThan(80); // Eng mos uy uchun yuqori ball
            rec.MatchScore.Should().BeLessOrEqualTo(100);
            rec.MatchScore.Should().BeGreaterOrEqualTo(0);
            rec.MatchReason.Should().Contain("Great price");
            rec.MatchReason.Should().Contain("Extra bedrooms");
            rec.MatchReason.Should().Contain("Pet-friendly");
            rec.MatchReason.Should().Contain("Private space");
        }
    }
}