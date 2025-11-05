//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Hosts;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfHostIsNullAndLogItAsync()
        {
            // given
            Host nullHost = null;

            var nullHostException =
                new NullHostException();

            var expectedHostValidationException =
                new HostValidationException(nullHostException);

            // when

            ValueTask<Host> addHostTask =
               this.hostService.AddHostAsync(nullHost);

            // then
            await Assert.ThrowsAsync<HostValidationException>(() =>
               addHostTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHostAsync(It.IsAny<Host>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfHostIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidHost = new Host
            {
                FirstName = invalidText
            };
            var invalidHostException = new InvalidHostException();
            invalidHostException.AddData(
                nameof(Host.FirstName),
                "Text is required");

            invalidHostException.AddData(
                nameof(Host.LastName),
                "Text is required");

            invalidHostException.AddData(
                nameof(Host.Email),
                "Text is required");
            invalidHostException.AddData(
                nameof(Host.DateOfBirth),
                "Date is required");

            var expectedHostValidationException =
                new HostValidationException(invalidHostException);

            // when
            ValueTask<Host> addHostTask =
                this.hostService.AddHostAsync(invalidHost);

            // then
            await Assert.ThrowsAsync<HostValidationException>(() =>
               addHostTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHostAsync(It.IsAny<Host>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGenderIsInvalidAndLogItAsync()
        {
            Host randomHost = CreateRandomHost();
            Host invalidHost = randomHost;
            invalidHost.Gender = GetInvalidEnum<GenderType>();
            var invalidHostException = new InvalidHostException();

            invalidHostException.AddData(
               key: nameof(Host.Gender),
               values: "Invalid gender value");
            var expectedHostValidationException =
                new HostValidationException(invalidHostException);
            // when
            ValueTask<Host> addHostTask =
                this.hostService.AddHostAsync(invalidHost);

            // then
            await Assert.ThrowsAsync<HostValidationException>(() =>
               addHostTask.AsTask());

            this.loggingBrokerMock.Verify(Broker =>
                Broker.LogError(It.Is(SameExceptionAs(
                    expectedHostValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHostAsync(It.IsAny<Host>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
