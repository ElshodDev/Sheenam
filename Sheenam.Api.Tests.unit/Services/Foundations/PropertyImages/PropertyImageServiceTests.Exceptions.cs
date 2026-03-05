//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.PropertyImages;
using Sheenam.Api.Models.Foundations.PropertyImages.Exceptions;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.PropertyImages
{
    public partial class PropertyImageServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            PropertyImage somePropertyImage = CreateRandomPropertyImage();
            SqlException sqlException = GetSqlError();

            var failedPropertyImageStorageException =
                new FailedPropertyImageStorageException(sqlException);

            var expectedPropertyImageDependencyException =
                new PropertyImageDependencyException(failedPropertyImageStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertyImageAsync(somePropertyImage))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<PropertyImage> addPropertyImageTask =
                this.propertyImageService.AddPropertyImageAsync(somePropertyImage);

            PropertyImageDependencyException actualPropertyImageDependencyException =
                await Assert.ThrowsAsync<PropertyImageDependencyException>(() =>
                    addPropertyImageTask.AsTask());

            // then
            actualPropertyImageDependencyException.Should().BeEquivalentTo(
                expectedPropertyImageDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyImageAsync(somePropertyImage),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPropertyImageDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfUnexpectedErrorOccursAndLogItAsync()
        {
            // given
            PropertyImage somePropertyImage = CreateRandomPropertyImage();
            var serviceException = new Exception();

            var failedPropertyImageServiceException =
                new FailedPropertyImageServiceException(serviceException);

            var expectedPropertyImageServiceException =
                new PropertyImageServiceException(failedPropertyImageServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertyImageAsync(somePropertyImage))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<PropertyImage> addPropertyImageTask =
                this.propertyImageService.AddPropertyImageAsync(somePropertyImage);

            PropertyImageServiceException actualPropertyImageServiceException =
                await Assert.ThrowsAsync<PropertyImageServiceException>(() =>
                    addPropertyImageTask.AsTask());

            // then
            actualPropertyImageServiceException.Should().BeEquivalentTo(
                expectedPropertyImageServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyImageAsync(somePropertyImage),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertyImageServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
