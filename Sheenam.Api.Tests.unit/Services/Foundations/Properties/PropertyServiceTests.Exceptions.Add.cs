//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.Properties;
using Sheenam.Api.Models.Foundations.Properties.Exceptions;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Properties
{
    public partial class PropertyServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Property someProperty = CreateRandomProperty();
            SqlException sqlException = GetSqlError();

            var failedPropertyStorageException =
                new FailedPropertyStorageException(sqlException);

            var expectedPropertyDependencyException =
                new PropertyDependencyException(failedPropertyStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertyAsync(someProperty))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Property> addPropertyTask =
                this.propertyService.AddPropertyAsync(someProperty);

            PropertyDependencyException actualPropertyDependencyException =
                await Assert.ThrowsAsync<PropertyDependencyException>(() =>
                    addPropertyTask.AsTask());

            // then
            actualPropertyDependencyException.Should().BeEquivalentTo(
                expectedPropertyDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyAsync(someProperty),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPropertyDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfUnexpectedErrorOccursAndLogItAsync()
        {
            // given
            Property someProperty = CreateRandomProperty();
            var serviceException = new Exception();

            var failedPropertyServiceException =
                new FailedPropertyServiceException(serviceException);

            var expectedPropertyServiceException =
                new PropertyServiceException(failedPropertyServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertyAsync(someProperty))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Property> addPropertyTask =
                this.propertyService.AddPropertyAsync(someProperty);

            PropertyServiceException actualPropertyServiceException =
                await Assert.ThrowsAsync<PropertyServiceException>(() =>
                    addPropertyTask.AsTask());

            // then
            actualPropertyServiceException.Should().BeEquivalentTo(
                expectedPropertyServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyAsync(someProperty),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertyServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}