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
        public async Task ShouldThrowValidationExceptionOnModifyIfHostIsNullAndLogItAsync()
        {
            // given
            HostModel nullHost = null;
            var nullHostException = new NullHostException();

            var expectedHostValidationException =
                new HostValidationException(nullHostException);

            // when
            ValueTask<HostModel> modifyHostTask =
                this.hostService.ModifyHostAsync(nullHost);

            // then
            await Assert.ThrowsAsync<HostValidationException>(() =>
                modifyHostTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PutHostAsync(It.IsAny<HostModel>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnModifyIfHostIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidHost = new HostModel
            {
                Id = Guid.Empty,
                FirstName = invalidText,
                LastName = invalidText,
                Email = invalidText,
                PhoneNumber = invalidText,
                DateOfBirth = default
            };

            var invalidHostException = new InvalidHostException();

            invalidHostException.AddData(
                key: nameof(HostModel.Id),
                values: "Id is required");

            invalidHostException.AddData(
                key: nameof(HostModel.FirstName),
                values: "Text is required");

            invalidHostException.AddData(
                key: nameof(HostModel.LastName),
                values: "Text is required");

            invalidHostException.AddData(
                key: nameof(HostModel.DateOfBirth),
                values: "Date is required");

            invalidHostException.AddData(
                key: nameof(HostModel.Email),
                values: "Text is required");

            invalidHostException.AddData(
                key: nameof(HostModel.PhoneNumber),
                values: "Text is required");

            var expectedHostValidationException =
                new HostValidationException(invalidHostException);

            // when
            ValueTask<HostModel> modifyHostTask =
                this.hostService.ModifyHostAsync(invalidHost);

            // then
            await Assert.ThrowsAsync<HostValidationException>(() =>
                modifyHostTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PutHostAsync(It.IsAny<HostModel>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
