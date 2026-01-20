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
    }
}