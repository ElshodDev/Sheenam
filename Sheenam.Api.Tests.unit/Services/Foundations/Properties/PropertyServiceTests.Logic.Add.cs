//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Properties;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Properties
{
    public partial class PropertyServiceTests
    {
        [Fact]
        public async Task ShouldAddPropertyAsync()
        {
            // given
            Property randomProperty = CreateRandomProperty();
            Property inputProperty = randomProperty;
            Property storageProperty = inputProperty;
            Property expectedProperty = storageProperty.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertyAsync(inputProperty))
                    .ReturnsAsync(storageProperty);

            // when
            Property actualProperty =
                await this.propertyService.AddPropertyAsync(inputProperty);

            // then
            actualProperty.Should().BeEquivalentTo(expectedProperty);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyAsync(inputProperty),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrievePropertyByIdAsync()
        {
            // given
            Guid randomPropertyId = Guid.NewGuid();
            Property randomProperty = CreateRandomProperty();
            Property storageProperty = randomProperty;
            Property expectedProperty = storageProperty.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPropertyByIdAsync(randomPropertyId))
                    .ReturnsAsync(storageProperty);

            // when
            Property actualProperty =
                await this.propertyService.RetrievePropertyByIdAsync(randomPropertyId);

            // then
            actualProperty.Should().BeEquivalentTo(expectedProperty);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPropertyByIdAsync(randomPropertyId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}