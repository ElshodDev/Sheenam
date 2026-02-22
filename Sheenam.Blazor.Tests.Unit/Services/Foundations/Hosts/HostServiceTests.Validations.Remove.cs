//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Blazor.Models.Foundations.Hosts.Exceptions;
using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidHostId = Guid.Empty;
            var invalidHostException = new InvalidHostException();

            invalidHostException.AddData(
                key: nameof(HostModel.Id),
                values: "Id is required");

            var expectedHostValidationException =
                new HostValidationException(invalidHostException);

            // when
            ValueTask<HostModel> removeHostByIdTask =
                this.hostService.RemoveHostByIdAsync(invalidHostId);

            // then
            await Assert.ThrowsAsync<HostValidationException>(() =>
                removeHostByIdTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteHostByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
