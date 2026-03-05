//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.PropertyImages;
using Sheenam.Api.Models.Foundations.PropertyImages.Exceptions;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.PropertyImages
{
    public partial class PropertyImageServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPropertyImageIsNullAndLogItAsync()
        {
            // given
            PropertyImage nullPropertyImage = null;
            var nullPropertyImageException = new NullPropertyImageException();

            var expectedPropertyImageValidationException =
                new PropertyImageValidationException(nullPropertyImageException);

            // when
            ValueTask<PropertyImage> addPropertyImageTask =
                this.propertyImageService.AddPropertyImageAsync(nullPropertyImage);

            // then
            await Assert.ThrowsAsync<PropertyImageValidationException>(() =>
                addPropertyImageTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertyImageValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyImageAsync(It.IsAny<PropertyImage>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPropertyImageIsInvalidAndLogItAsync()
        {
            // given
            var invalidPropertyImage = new PropertyImage
            {
                Id = Guid.Empty,
                ImageUrl = string.Empty,
                CreatedDate = default
            };

            var invalidPropertyImageException = new InvalidPropertyImageException();

            invalidPropertyImageException.AddData(
                key: nameof(PropertyImage.Id),
                values: "Id is required");

            invalidPropertyImageException.AddData(
                key: nameof(PropertyImage.ImageUrl),
                values: "Text is required");

            invalidPropertyImageException.AddData(
                key: nameof(PropertyImage.CreatedDate),
                values: "Date is required");

            var expectedPropertyImageValidationException =
                new PropertyImageValidationException(invalidPropertyImageException);

            // when
            ValueTask<PropertyImage> addPropertyImageTask =
                this.propertyImageService.AddPropertyImageAsync(invalidPropertyImage);

            // then
            await Assert.ThrowsAsync<PropertyImageValidationException>(() =>
                addPropertyImageTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertyImageValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyImageAsync(It.IsAny<PropertyImage>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidPropertyImageId = Guid.Empty;
            var invalidPropertyImageException = new InvalidPropertyImageException();

            invalidPropertyImageException.AddData(
                key: nameof(PropertyImage.Id),
                values: "Id is required");

            var expectedPropertyImageValidationException =
                new PropertyImageValidationException(invalidPropertyImageException);

            // when
            ValueTask<PropertyImage> retrievePropertyImageTask =
                this.propertyImageService.RetrievePropertyImageByIdAsync(invalidPropertyImageId);

            // then
            await Assert.ThrowsAsync<PropertyImageValidationException>(() =>
                retrievePropertyImageTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertyImageValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPropertyImageByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfNotFoundAndLogItAsync()
        {
            // given
            Guid somePropertyImageId = Guid.NewGuid();
            PropertyImage noPropertyImage = null;

            var notFoundPropertyImageException =
                new NotFoundPropertyImageException(somePropertyImageId);

            var expectedPropertyImageValidationException =
                new PropertyImageValidationException(notFoundPropertyImageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPropertyImageByIdAsync(somePropertyImageId))
                    .ReturnsAsync(noPropertyImage);

            // when
            ValueTask<PropertyImage> retrievePropertyImageTask =
                this.propertyImageService.RetrievePropertyImageByIdAsync(somePropertyImageId);

            // then
            await Assert.ThrowsAsync<PropertyImageValidationException>(() =>
                retrievePropertyImageTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPropertyImageByIdAsync(somePropertyImageId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertyImageValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
