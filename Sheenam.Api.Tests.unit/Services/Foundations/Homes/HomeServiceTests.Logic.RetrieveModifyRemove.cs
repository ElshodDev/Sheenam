//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Homes;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllHomes()
        {
            // given
            IQueryable<Home> randomHomes = CreateRandomHomes();
            IQueryable<Home> storageHomes = randomHomes;
            IQueryable<Home> expectedHomes = storageHomes.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Returns(storageHomes);

            // when
            IQueryable<Home> actualHomes =
                this.homeService.RetrieveAllHomes();

            // then
            actualHomes.Should().BeEquivalentTo(expectedHomes);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomes(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveHomeByIdAsync()
        {
            // given
            Guid randomHomeId = Guid.NewGuid();
            Home randomHome = CreateRandomHome();
            Home storageHome = randomHome;
            Home expectedHome = storageHome.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(randomHomeId))
                    .ReturnsAsync(storageHome);

            // when
            Home actualHome =
                await this.homeService.RetrieveHomeByIdAsync(randomHomeId);

            // then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(randomHomeId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyHomeAsync()
        {
            // given
            Home randomHome = CreateRandomHome();
            randomHome.CreatedDate = DateTimeOffset.UtcNow.AddDays(-1);
            randomHome.UpdatedDate = DateTimeOffset.UtcNow;
            Home inputHome = randomHome;
            Home storageHome = inputHome.DeepClone();
            Home expectedHome = storageHome.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(inputHome.Id))
                    .ReturnsAsync(storageHome);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateHomeAsync(inputHome))
                    .ReturnsAsync(storageHome);

            // when
            Home actualHome =
                await this.homeService.ModifyHomeAsync(inputHome);

            // then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(inputHome.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeAsync(inputHome),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveHomeByIdAsync()
        {
            // given
            Guid randomHomeId = Guid.NewGuid();
            Home randomHome = CreateRandomHome();
            Home storageHome = randomHome;
            Home expectedHome = storageHome.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(randomHomeId))
                    .ReturnsAsync(storageHome);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteHomeAsync(storageHome))
                    .ReturnsAsync(expectedHome);

            // when
            Home actualHome =
                await this.homeService.RemoveHomeByIdAsync(randomHomeId);

            // then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(randomHomeId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteHomeAsync(storageHome),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
