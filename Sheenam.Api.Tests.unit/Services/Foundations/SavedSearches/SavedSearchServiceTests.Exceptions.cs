//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.SavedSearches;
using Sheenam.Api.Models.Foundations.SavedSearches.Exceptions;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.SavedSearches
{
    public partial class SavedSearchServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            SavedSearch someSavedSearch = CreateRandomSavedSearch();
            SqlException sqlException = GetSqlError();

            var failedSavedSearchStorageException =
                new FailedSavedSearchStorageException(sqlException);

            var expectedSavedSearchDependencyException =
                new SavedSearchDependencyException(failedSavedSearchStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertSavedSearchAsync(someSavedSearch))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<SavedSearch> addSavedSearchTask =
                this.savedSearchService.AddSavedSearchAsync(someSavedSearch);

            SavedSearchDependencyException actualSavedSearchDependencyException =
                await Assert.ThrowsAsync<SavedSearchDependencyException>(() =>
                    addSavedSearchTask.AsTask());

            // then
            actualSavedSearchDependencyException.Should().BeEquivalentTo(
                expectedSavedSearchDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSavedSearchAsync(someSavedSearch),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedSavedSearchDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfUnexpectedErrorOccursAndLogItAsync()
        {
            // given
            SavedSearch someSavedSearch = CreateRandomSavedSearch();
            var serviceException = new Exception();

            var failedSavedSearchServiceException =
                new FailedSavedSearchServiceException(serviceException);

            var expectedSavedSearchServiceException =
                new SavedSearchServiceException(failedSavedSearchServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertSavedSearchAsync(someSavedSearch))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<SavedSearch> addSavedSearchTask =
                this.savedSearchService.AddSavedSearchAsync(someSavedSearch);

            SavedSearchServiceException actualSavedSearchServiceException =
                await Assert.ThrowsAsync<SavedSearchServiceException>(() =>
                    addSavedSearchTask.AsTask());

            // then
            actualSavedSearchServiceException.Should().BeEquivalentTo(
                expectedSavedSearchServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSavedSearchAsync(someSavedSearch),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSavedSearchServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
