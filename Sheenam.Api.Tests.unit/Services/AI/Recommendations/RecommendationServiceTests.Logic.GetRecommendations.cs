//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.AI.Recommendations;
using Sheenam.Api.Models.Foundations.Homes;

namespace Sheenam.Api.Tests.Unit.Services.AI.Recommendations
{
    public partial class RecommendationServiceTests
    {
        [Fact]
        public async Task ShouldGetRecommendedHomesAsync()
        {
            // given
            IQueryable<Home> randomHomes = CreateRandomHomes();
            IQueryable<Home> storageHomes = randomHomes.DeepClone();
            IQueryable<Home> expectedHomes = storageHomes;

            var preferences = new GuestPreferences
            {
                MaxPrice = 1500,
                MinBedrooms = 2,
                NeedsPetAllowed = true,
                PreferPrivate = false
            };

            int topN = 5;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Returns(storageHomes);

            // when
            List<HomeRecommendation> actualRecommendations =
                await this.recommendationService.GetRecommendedHomesAsync(preferences, topN);

            // then
            actualRecommendations.Should().NotBeNull();
            actualRecommendations.Count.Should().BeLessOrEqualTo(topN);

            // Verify descending order manually
            for (int i = 0; i < actualRecommendations.Count - 1; i++)
            {
                actualRecommendations[i].MatchScore.Should()
                    .BeGreaterOrEqualTo(actualRecommendations[i + 1].MatchScore);
            }

            actualRecommendations.ForEach(recommendation =>
            {
                recommendation.MatchScore.Should().BeGreaterOrEqualTo(0);
                recommendation.MatchScore.Should().BeLessOrEqualTo(100);
                recommendation.MatchReason.Should().NotBeNullOrEmpty();
                recommendation.HomeId.Should().NotBeEmpty();
            });

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomes(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldFilterHomesByPreferencesAsync()
        {
            // given
            var vacantHomes = new List<Home>
            {
                new Home
                {
                    Id = Guid.NewGuid(),
                    IsVacant = true,
                    Price = 1000,
                    NumberOfBedrooms = 3,
                    IsPetAllowed = true,
                    IsShared = false
                },
                new Home
                {
                    Id = Guid.NewGuid(),
                    IsVacant = true,
                    Price = 2000,
                    NumberOfBedrooms = 2,
                    IsPetAllowed = true,
                    IsShared = false
                },
                new Home
                {
                    Id = Guid.NewGuid(),
                    IsVacant = true,
                    Price = 800,
                    NumberOfBedrooms = 1,
                    IsPetAllowed = true,
                    IsShared = false
                }
            }.AsQueryable();

            var preferences = new GuestPreferences
            {
                MaxPrice = 1500,
                MinBedrooms = 2,
                NeedsPetAllowed = true,
                PreferPrivate = false
            };

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Returns(vacantHomes);

            // when
            List<HomeRecommendation> actualRecommendations =
                await this.recommendationService.GetRecommendedHomesAsync(preferences);

            // then
            actualRecommendations.Should().HaveCount(1);
            actualRecommendations.First().Price.Should().Be(1000);
            actualRecommendations.First().NumberOfBedrooms.Should().BeGreaterOrEqualTo(2);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomes(),
                    Times.Once);
        }

        [Fact]
        public async Task ShouldReturnEmptyListWhenNoHomesMatchAsync()
        {
            // given
            var vacantHomes = new List<Home>
            {
                new Home
                {
                    IsVacant = true,
                    Price = 5000,
                    NumberOfBedrooms = 1,
                    IsPetAllowed = false
                }
            }.AsQueryable();

            var preferences = new GuestPreferences
            {
                MaxPrice = 1000,
                MinBedrooms = 3,
                NeedsPetAllowed = true
            };

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Returns(vacantHomes);

            // when
            List<HomeRecommendation> actualRecommendations =
                await this.recommendationService.GetRecommendedHomesAsync(preferences);

            // then
            actualRecommendations.Should().BeEmpty();
        }

        [Fact]
        public async Task ShouldRankRecommendationsByScoreAsync()
        {
            // given
            var vacantHomes = new List<Home>
            {
                new Home
                {
                    Id = Guid.NewGuid(),
                    IsVacant = true,
                    Price = 500,
                    NumberOfBedrooms = 4,
                    IsPetAllowed = true,
                    IsShared = false
                },
                new Home
                {
                    Id = Guid.NewGuid(),
                    IsVacant = true,
                    Price = 1200,
                    NumberOfBedrooms = 2,
                    IsPetAllowed = true,
                    IsShared = true
                }
            }.AsQueryable();

            var preferences = new GuestPreferences
            {
                MaxPrice = 1500,
                MinBedrooms = 2,
                NeedsPetAllowed = true
            };

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Returns(vacantHomes);

            // when
            List<HomeRecommendation> actualRecommendations =
                await this.recommendationService.GetRecommendedHomesAsync(preferences);

            // then
            actualRecommendations.Should().HaveCount(2);
            actualRecommendations[0].Rank.Should().Be(1);
            actualRecommendations[1].Rank.Should().Be(2);
            actualRecommendations[0].MatchScore.Should()
                .BeGreaterOrEqualTo(actualRecommendations[1].MatchScore);
        }
    }
}