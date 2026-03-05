//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.PropertyImages;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.PropertyImages
{
    public partial class PropertyImageServiceTests
    {
        [Fact]
        public async Task ShouldAddPropertyImageAsync()
        {
            // given
            PropertyImage randomPropertyImage = CreateRandomPropertyImage();
            PropertyImage inputPropertyImage = randomPropertyImage;
            PropertyImage storagePropertyImage = inputPropertyImage;
            PropertyImage expectedPropertyImage = storagePropertyImage.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertyImageAsync(inputPropertyImage))
                    .ReturnsAsync(storagePropertyImage);

            // when
            PropertyImage actualPropertyImage =
                await this.propertyImageService.AddPropertyImageAsync(inputPropertyImage);

            // then
            actualPropertyImage.Should().BeEquivalentTo(expectedPropertyImage);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyImageAsync(inputPropertyImage),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrievePropertyImageByIdAsync()
        {
            // given
            Guid randomPropertyImageId = Guid.NewGuid();
            PropertyImage randomPropertyImage = CreateRandomPropertyImage();
            PropertyImage storagePropertyImage = randomPropertyImage;
            PropertyImage expectedPropertyImage = storagePropertyImage.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPropertyImageByIdAsync(randomPropertyImageId))
                    .ReturnsAsync(storagePropertyImage);

            // when
            PropertyImage actualPropertyImage =
                await this.propertyImageService
                    .RetrievePropertyImageByIdAsync(randomPropertyImageId);

            // then
            actualPropertyImage.Should().BeEquivalentTo(expectedPropertyImage);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPropertyImageByIdAsync(randomPropertyImageId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyPropertyImageAsync()
        {
            // given
            PropertyImage randomPropertyImage = CreateRandomPropertyImage();
            PropertyImage inputPropertyImage = randomPropertyImage;
            PropertyImage storagePropertyImage = inputPropertyImage.DeepClone();
            PropertyImage updatedPropertyImage = inputPropertyImage;
            PropertyImage expectedPropertyImage = updatedPropertyImage.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPropertyImageByIdAsync(inputPropertyImage.Id))
                    .ReturnsAsync(storagePropertyImage);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdatePropertyImageAsync(inputPropertyImage))
                    .ReturnsAsync(updatedPropertyImage);

            // when
            PropertyImage actualPropertyImage =
                await this.propertyImageService.ModifyPropertyImageAsync(inputPropertyImage);

            // then
            actualPropertyImage.Should().BeEquivalentTo(expectedPropertyImage);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPropertyImageByIdAsync(inputPropertyImage.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdatePropertyImageAsync(inputPropertyImage),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemovePropertyImageByIdAsync()
        {
            // given
            Guid randomPropertyImageId = Guid.NewGuid();
            PropertyImage randomPropertyImage = CreateRandomPropertyImage();
            PropertyImage storagePropertyImage = randomPropertyImage;
            PropertyImage expectedPropertyImage = storagePropertyImage.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPropertyImageByIdAsync(randomPropertyImageId))
                    .ReturnsAsync(storagePropertyImage);

            this.storageBrokerMock.Setup(broker =>
                broker.DeletePropertyImageAsync(storagePropertyImage))
                    .ReturnsAsync(expectedPropertyImage);

            // when
            PropertyImage actualPropertyImage =
                await this.propertyImageService
                    .RemovePropertyImageByIdAsync(randomPropertyImageId);

            // then
            actualPropertyImage.Should().BeEquivalentTo(expectedPropertyImage);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPropertyImageByIdAsync(randomPropertyImageId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeletePropertyImageAsync(storagePropertyImage),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
