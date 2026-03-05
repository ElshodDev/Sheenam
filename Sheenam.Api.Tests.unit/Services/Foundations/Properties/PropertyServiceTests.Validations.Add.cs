//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.Properties;
using Sheenam.Api.Models.Foundations.Properties.Exceptions;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Properties
{
    public partial class PropertyServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPropertyIsNullAndLogItAsync()
        {
            // given
            Property nullProperty = null;
            var nullPropertyException = new NullPropertyException();

            var expectedPropertyValidationException =
                new PropertyValidationException(nullPropertyException);

            // when
            ValueTask<Property> addPropertyTask =
                this.propertyService.AddPropertyAsync(nullProperty);

            // then
            await Assert.ThrowsAsync<PropertyValidationException>(() =>
                addPropertyTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyAsync(It.IsAny<Property>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPropertyIsInvalidAndLogItAsync()
        {
            // given
            var invalidProperty = new Property
            {
                Id = Guid.Empty,
                AgentId = Guid.Empty,
                Title = string.Empty,
                Address = string.Empty,
                City = string.Empty,
                CreatedDate = default,
                UpdatedDate = default
            };

            var invalidPropertyException = new InvalidPropertyException();

            invalidPropertyException.AddData(
                key: nameof(Property.Id),
                values: "Id is required");

            invalidPropertyException.AddData(
                key: nameof(Property.AgentId),
                values: "Id is required");

            invalidPropertyException.AddData(
                key: nameof(Property.Title),
                values: "Text is required");

            invalidPropertyException.AddData(
                key: nameof(Property.Address),
                values: "Text is required");

            invalidPropertyException.AddData(
                key: nameof(Property.City),
                values: "Text is required");

            invalidPropertyException.AddData(
                key: nameof(Property.CreatedDate),
                values: "Date is required");

            invalidPropertyException.AddData(
                key: nameof(Property.UpdatedDate),
                values: "Date is required");

            var expectedPropertyValidationException =
                new PropertyValidationException(invalidPropertyException);

            // when
            ValueTask<Property> addPropertyTask =
                this.propertyService.AddPropertyAsync(invalidProperty);

            // then
            await Assert.ThrowsAsync<PropertyValidationException>(() =>
                addPropertyTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyAsync(It.IsAny<Property>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}