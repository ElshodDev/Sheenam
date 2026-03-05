//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.SavedSearches;
using Sheenam.Api.Models.Foundations.SavedSearches.Exceptions;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.SavedSearches
{
    public partial class SavedSearchServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfSavedSearchIsNullAndLogItAsync()
        {
            // given
            SavedSearch nullSavedSearch = null;
            var nullSavedSearchException = new NullSavedSearchException();

            var expectedSavedSearchValidationException =
                new SavedSearchValidationException(nullSavedSearchException);

            // when
            ValueTask<SavedSearch> addSavedSearchTask =
                this.savedSearchService.AddSavedSearchAsync(nullSavedSearch);

            // then
            await Assert.ThrowsAsync<SavedSearchValidationException>(() =>
                addSavedSearchTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSavedSearchValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSavedSearchAsync(It.IsAny<SavedSearch>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfSavedSearchIsInvalidAndLogItAsync()
        {
            // given
            var invalidSavedSearch = new SavedSearch
            {
                Id = Guid.Empty,
                UserId = Guid.Empty,
                Name = string.Empty,
                CreatedDate = default
            };

            var invalidSavedSearchException = new InvalidSavedSearchException();

            invalidSavedSearchException.AddData(
                key: nameof(SavedSearch.Id),
                values: "Id is required");

            invalidSavedSearchException.AddData(
                key: nameof(SavedSearch.UserId),
                values: "Id is required");

            invalidSavedSearchException.AddData(
                key: nameof(SavedSearch.Name),
                values: "Text is required");

            invalidSavedSearchException.AddData(
                key: nameof(SavedSearch.CreatedDate),
                values: "Date is required");

            var expectedSavedSearchValidationException =
                new SavedSearchValidationException(invalidSavedSearchException);

            // when
            ValueTask<SavedSearch> addSavedSearchTask =
                this.savedSearchService.AddSavedSearchAsync(invalidSavedSearch);

            // then
            await Assert.ThrowsAsync<SavedSearchValidationException>(() =>
                addSavedSearchTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSavedSearchValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSavedSearchAsync(It.IsAny<SavedSearch>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidSavedSearchId = Guid.Empty;
            var invalidSavedSearchException = new InvalidSavedSearchException();

            invalidSavedSearchException.AddData(
                key: nameof(SavedSearch.Id),
                values: "Id is required");

            var expectedSavedSearchValidationException =
                new SavedSearchValidationException(invalidSavedSearchException);

            // when
            ValueTask<SavedSearch> retrieveSavedSearchTask =
                this.savedSearchService.RetrieveSavedSearchByIdAsync(invalidSavedSearchId);

            // then
            await Assert.ThrowsAsync<SavedSearchValidationException>(() =>
                retrieveSavedSearchTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSavedSearchValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSavedSearchByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfNotFoundAndLogItAsync()
        {
            // given
            Guid someSavedSearchId = Guid.NewGuid();
            SavedSearch noSavedSearch = null;

            var notFoundSavedSearchException =
                new NotFoundSavedSearchException(someSavedSearchId);

            var expectedSavedSearchValidationException =
                new SavedSearchValidationException(notFoundSavedSearchException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSavedSearchByIdAsync(someSavedSearchId))
                    .ReturnsAsync(noSavedSearch);

            // when
            ValueTask<SavedSearch> retrieveSavedSearchTask =
                this.savedSearchService.RetrieveSavedSearchByIdAsync(someSavedSearchId);

            // then
            await Assert.ThrowsAsync<SavedSearchValidationException>(() =>
                retrieveSavedSearchTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSavedSearchByIdAsync(someSavedSearchId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSavedSearchValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
