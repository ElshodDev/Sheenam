//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.SavedSearches;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.SavedSearches
{
    public partial class SavedSearchServiceTests
    {
        [Fact]
        public async Task ShouldAddSavedSearchAsync()
        {
            // given
            SavedSearch randomSavedSearch = CreateRandomSavedSearch();
            SavedSearch inputSavedSearch = randomSavedSearch;
            SavedSearch storageSavedSearch = inputSavedSearch;
            SavedSearch expectedSavedSearch = storageSavedSearch.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertSavedSearchAsync(inputSavedSearch))
                    .ReturnsAsync(storageSavedSearch);

            // when
            SavedSearch actualSavedSearch =
                await this.savedSearchService.AddSavedSearchAsync(inputSavedSearch);

            // then
            actualSavedSearch.Should().BeEquivalentTo(expectedSavedSearch);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSavedSearchAsync(inputSavedSearch),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveSavedSearchByIdAsync()
        {
            // given
            Guid randomSavedSearchId = Guid.NewGuid();
            SavedSearch randomSavedSearch = CreateRandomSavedSearch();
            SavedSearch storageSavedSearch = randomSavedSearch;
            SavedSearch expectedSavedSearch = storageSavedSearch.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSavedSearchByIdAsync(randomSavedSearchId))
                    .ReturnsAsync(storageSavedSearch);

            // when
            SavedSearch actualSavedSearch =
                await this.savedSearchService.RetrieveSavedSearchByIdAsync(randomSavedSearchId);

            // then
            actualSavedSearch.Should().BeEquivalentTo(expectedSavedSearch);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSavedSearchByIdAsync(randomSavedSearchId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveSavedSearchByIdAsync()
        {
            // given
            Guid randomSavedSearchId = Guid.NewGuid();
            SavedSearch randomSavedSearch = CreateRandomSavedSearch();
            SavedSearch storageSavedSearch = randomSavedSearch;
            SavedSearch expectedSavedSearch = storageSavedSearch.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSavedSearchByIdAsync(randomSavedSearchId))
                    .ReturnsAsync(storageSavedSearch);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteSavedSearchAsync(storageSavedSearch))
                    .ReturnsAsync(expectedSavedSearch);

            // when
            SavedSearch actualSavedSearch =
                await this.savedSearchService.RemoveSavedSearchByIdAsync(randomSavedSearchId);

            // then
            actualSavedSearch.Should().BeEquivalentTo(expectedSavedSearch);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSavedSearchByIdAsync(randomSavedSearchId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteSavedSearchAsync(storageSavedSearch),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
