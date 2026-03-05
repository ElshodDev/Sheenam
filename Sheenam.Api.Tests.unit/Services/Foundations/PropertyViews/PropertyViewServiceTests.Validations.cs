//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.PropertyViews;
using Sheenam.Api.Models.Foundations.PropertyViews.Exceptions;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.PropertyViews
{
    public partial class PropertyViewServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPropertyViewIsNullAndLogItAsync()
        {
            // given
            PropertyView nullPropertyView = null;
            var nullPropertyViewException = new NullPropertyViewException();

            var expectedPropertyViewValidationException =
                new PropertyViewValidationException(nullPropertyViewException);

            // when
            ValueTask<PropertyView> addPropertyViewTask =
                this.propertyViewService.AddPropertyViewAsync(nullPropertyView);

            // then
            await Assert.ThrowsAsync<PropertyViewValidationException>(() =>
                addPropertyViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertyViewValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyViewAsync(It.IsAny<PropertyView>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPropertyViewIsInvalidAndLogItAsync()
        {
            // given
            var invalidPropertyView = new PropertyView
            {
                Id = Guid.Empty,
                ViewedDate = default
            };

            var invalidPropertyViewException = new InvalidPropertyViewException();

            invalidPropertyViewException.AddData(
                key: nameof(PropertyView.Id),
                values: "Id is required");

            invalidPropertyViewException.AddData(
                key: nameof(PropertyView.ViewedDate),
                values: "Date is required");

            var expectedPropertyViewValidationException =
                new PropertyViewValidationException(invalidPropertyViewException);

            // when
            ValueTask<PropertyView> addPropertyViewTask =
                this.propertyViewService.AddPropertyViewAsync(invalidPropertyView);

            // then
            await Assert.ThrowsAsync<PropertyViewValidationException>(() =>
                addPropertyViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertyViewValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyViewAsync(It.IsAny<PropertyView>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidPropertyViewId = Guid.Empty;
            var invalidPropertyViewException = new InvalidPropertyViewException();

            invalidPropertyViewException.AddData(
                key: nameof(PropertyView.Id),
                values: "Id is required");

            var expectedPropertyViewValidationException =
                new PropertyViewValidationException(invalidPropertyViewException);

            // when
            ValueTask<PropertyView> retrievePropertyViewTask =
                this.propertyViewService.RetrievePropertyViewByIdAsync(invalidPropertyViewId);

            // then
            await Assert.ThrowsAsync<PropertyViewValidationException>(() =>
                retrievePropertyViewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertyViewValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPropertyViewByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
