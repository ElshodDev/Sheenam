//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Homes;

namespace Sheenam.Api.Tests.unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldAddHomeAsync()
        {
            //given 
            Home randomHome = CreateRandomHome();
            Home inputHome = randomHome;
            Home StorageHome = inputHome;
            Home expectedHome = StorageHome.DeepClone();


            this.storageBrokerMock.Setup(broker =>
                     broker.InsertHomeAsync(inputHome))
                         .ReturnsAsync(StorageHome);
            //when
            Home actualHome =
               await this.homeService.AddHomeAsync(inputHome);

            //then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeAsync(inputHome),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnHomeWhenHomeExistsAsync()
        {
            // given
            Home randomHome = CreateRandomHome();
            Guid homeId = randomHome.Id;
            Home storageHome = randomHome;
            Home expectedHome = storageHome.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(homeId))
                .ReturnsAsync(storageHome);

            // when
            Home actualHome =
                await this.homeService.RetrieveHomeByIdAsync(homeId);

            // then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(homeId),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldRetrieveAllHomes()
        {
            // given
            IQueryable<Home> randomHomes = CreateRandomHomes();
            IQueryable<Home> storageHomes = randomHomes;
            IQueryable<Home> expectedHomes = storageHomes;

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
        public async Task ShouldModifyHomeAsync()
        {
            // given
            Home randomHome = CreateRandomHome();
            Home inputHome = randomHome;
            Home storageHome = inputHome;
            Home updatedHome = inputHome;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(inputHome.Id))
                    .ReturnsAsync(storageHome);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateHomeAsync(inputHome))
                    .ReturnsAsync(updatedHome);

            // when
            Home actualHome =
                await this.homeService.ModifyHomeAsync(inputHome);

            // then
            actualHome.Should().BeEquivalentTo(updatedHome);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(inputHome.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeAsync(inputHome),
                    Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
