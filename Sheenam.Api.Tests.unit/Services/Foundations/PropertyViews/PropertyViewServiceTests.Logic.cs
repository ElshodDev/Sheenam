//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.PropertyViews;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.PropertyViews
{
    public partial class PropertyViewServiceTests
    {
        [Fact]
        public async Task ShouldAddPropertyViewAsync()
        {
            // given
            PropertyView randomPropertyView = CreateRandomPropertyView();
            PropertyView inputPropertyView = randomPropertyView;
            PropertyView storagePropertyView = inputPropertyView;
            PropertyView expectedPropertyView = storagePropertyView.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertyViewAsync(inputPropertyView))
                    .ReturnsAsync(storagePropertyView);

            // when
            PropertyView actualPropertyView =
                await this.propertyViewService.AddPropertyViewAsync(inputPropertyView);

            // then
            actualPropertyView.Should().BeEquivalentTo(expectedPropertyView);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyViewAsync(inputPropertyView),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrievePropertyViewByIdAsync()
        {
            // given
            Guid randomPropertyViewId = Guid.NewGuid();
            PropertyView randomPropertyView = CreateRandomPropertyView();
            PropertyView storagePropertyView = randomPropertyView;
            PropertyView expectedPropertyView = storagePropertyView.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPropertyViewByIdAsync(randomPropertyViewId))
                    .ReturnsAsync(storagePropertyView);

            // when
            PropertyView actualPropertyView =
                await this.propertyViewService
                    .RetrievePropertyViewByIdAsync(randomPropertyViewId);

            // then
            actualPropertyView.Should().BeEquivalentTo(expectedPropertyView);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPropertyViewByIdAsync(randomPropertyViewId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemovePropertyViewByIdAsync()
        {
            // given
            Guid randomPropertyViewId = Guid.NewGuid();
            PropertyView randomPropertyView = CreateRandomPropertyView();
            PropertyView storagePropertyView = randomPropertyView;
            PropertyView expectedPropertyView = storagePropertyView.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPropertyViewByIdAsync(randomPropertyViewId))
                    .ReturnsAsync(storagePropertyView);

            this.storageBrokerMock.Setup(broker =>
                broker.DeletePropertyViewAsync(storagePropertyView))
                    .ReturnsAsync(expectedPropertyView);

            // when
            PropertyView actualPropertyView =
                await this.propertyViewService
                    .RemovePropertyViewByIdAsync(randomPropertyViewId);

            // then
            actualPropertyView.Should().BeEquivalentTo(expectedPropertyView);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPropertyViewByIdAsync(randomPropertyViewId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeletePropertyViewAsync(storagePropertyView),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
