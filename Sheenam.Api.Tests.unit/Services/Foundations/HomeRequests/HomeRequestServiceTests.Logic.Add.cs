//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.HomeRequests;

namespace Sheenam.Api.Tests.unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public async Task ShouldAddHomeRequestAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest inputHomeRequest = randomHomeRequest;
            HomeRequest storageHomeRequest = inputHomeRequest;
            HomeRequest expectedHomeRequest = storageHomeRequest.DeepClone();

            // ✅ YANGI: Status va RejectionReason default qiymatlari
            expectedHomeRequest.Status = HomeRequestStatus.Pending;
            expectedHomeRequest.RejectionReason = null;

            this.storageBrokerMock.Setup(broker =>
                broker.InsertHomeRequestAsync(inputHomeRequest))
                    .ReturnsAsync(storageHomeRequest);

            // when
            HomeRequest actualHomeRequest =
                await this.homeRequestService.AddHomeRequestAsync(inputHomeRequest);

            // then
            actualHomeRequest.Should().BeEquivalentTo(expectedHomeRequest);
            // ✅ YANGI:  Status Pending bo'lishi kerakligini tekshirish
            actualHomeRequest.Status.Should().Be(HomeRequestStatus.Pending);
            // ✅ YANGI:  RejectionReason null bo'lishi kerakligini tekshirish
            actualHomeRequest.RejectionReason.Should().BeNull();

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeRequestAsync(inputHomeRequest),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldRetrieveAllHosts()
        {
            // given
            IQueryable<HomeRequest> randomHomeRequests = CreateRandomHomeRequests();
            IQueryable<HomeRequest> storageHomeRequests = randomHomeRequests;
            IQueryable<HomeRequest> expectedHomeRequests = storageHomeRequests.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomeRequests())
                    .Returns(storageHomeRequests);

            // when
            IQueryable<HomeRequest> actualHomeRequests =
                this.homeRequestService.RetrieveAllHomeRequests();
            // then
            actualHomeRequests.Should().BeEquivalentTo(expectedHomeRequests);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomeRequests(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveHomeRequestByIdAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest storageHomeRequest = randomHomeRequest;

            HomeRequest expectedHomeRequest = storageHomeRequest.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(randomHomeRequest.Id))
                    .ReturnsAsync(storageHomeRequest);

            // when
            HomeRequest actualHomeRequest =
                await this.homeRequestService.RetrieveHomeRequestByIdAsync(randomHomeRequest.Id);

            // then
            actualHomeRequest.Should().BeEquivalentTo(expectedHomeRequest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(randomHomeRequest.Id),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnUpdatedHomeRequestWhenUpdateIsSuccessfulAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest inputHomeRequest = randomHomeRequest;
            HomeRequest storageHomeRequest = inputHomeRequest.DeepClone();
            HomeRequest expectedHomeRequest = storageHomeRequest.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(inputHomeRequest.Id))
                .ReturnsAsync(storageHomeRequest);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateHomeRequestAsync(inputHomeRequest))
                .ReturnsAsync(expectedHomeRequest);

            // when
            HomeRequest actualHomeRequest =
                await this.homeRequestService.ModifyHomeRequestAsync(inputHomeRequest);

            // then
            Assert.Equal(expectedHomeRequest, actualHomeRequest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(inputHomeRequest.Id),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeRequestAsync(inputHomeRequest),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldDeleteHostAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest inputHomeRequest = randomHomeRequest;
            HomeRequest storageHomeRequest = inputHomeRequest.DeepClone();
            HomeRequest expectedHomeRequest = storageHomeRequest.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(inputHomeRequest.Id))
                    .ReturnsAsync(storageHomeRequest);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteHomeRequestAsync(storageHomeRequest))
                    .ReturnsAsync(expectedHomeRequest);

            // when
            HomeRequest actualHomeRequest =
                await this.homeRequestService.RemoveHomeRequestByIdAsync(inputHomeRequest.Id);

            // then
            actualHomeRequest.Should().BeEquivalentTo(expectedHomeRequest);
            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(inputHomeRequest.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteHomeRequestAsync(storageHomeRequest),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}