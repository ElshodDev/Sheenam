//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Hosts;


namespace Sheenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        [Fact]
        public async Task ShouldAddHostAsync()
        {
            //given 
            Host randomHost = CreateRandomHost();
            Host inputHost = randomHost;
            Host storageHost = inputHost;
            Host expectedHost = storageHost.DeepClone();

            this.storageBrokerMock.Setup(broker =>
            broker.InsertHostAsync(inputHost))
                .ReturnsAsync(storageHost);
            //when

            Host actualHost =
                  await this.hostService.AddHostAsync(inputHost);

            //then
            actualHost.Should().BeEquivalentTo(expectedHost);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertHostAsync(inputHost),
            Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public void ShouldRetrieveAllHosts()
        {
            // given
            IQueryable<Host> randomHosts = CreateRandomHosts();
            IQueryable<Host> storageHosts = randomHosts;
            IQueryable<Host> expectedHosts = storageHosts.DeepClone();
            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHosts())
                    .Returns(storageHosts);
            // when
            IQueryable<Host> actualHosts =
                this.hostService.RetrieveAllHosts();
            // then
            actualHosts.Should().BeEquivalentTo(expectedHosts);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHosts(),
                    Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnHostWhenHostExistsAsync()
        {
            // given
            Host randomHost = CreateRandomHost();
            Guid hostId = randomHost.Id;

            this.storageBrokerMock
                .Setup(broker => broker.SelectHostByIdAsync(hostId))
                .ReturnsAsync(randomHost);

            // when
            Host actualHost = await this.hostService.RetrieveHostByIdAsync(hostId);
            // then
            Assert.Equal(randomHost, actualHost);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostByIdAsync(hostId), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnUpdatedHostWhenUpdateIsSuccessfulAsync()
        {
            // given
            Host someHost = CreateRandomHost();
            Guid hostId = someHost.Id;

            this.storageBrokerMock
                .Setup(broker => broker.SelectHostByIdAsync(someHost.Id))
                .ReturnsAsync(someHost);

            this.storageBrokerMock
                .Setup(broker => broker.UpdateHostAsync(someHost))
                .ReturnsAsync(someHost);

            // when
            Host updatedHost = await this.hostService.ModifyHostAsync(someHost);

            // then
            Assert.Equal(someHost, updatedHost);

            this.storageBrokerMock.Verify(broker =>
            broker.SelectHostByIdAsync(someHost.Id), Times.Once);

            this.storageBrokerMock.Verify(broker =>
            broker.UpdateHostAsync(someHost), Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();

        }
    }
}
