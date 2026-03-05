//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.Messages;
using Sheenam.Api.Models.Foundations.Messages.Exceptions;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Messages
{
    public partial class MessageServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Message someMessage = CreateRandomMessage();
            SqlException sqlException = GetSqlError();

            var failedMessageStorageException =
                new FailedMessageStorageException(sqlException);

            var expectedMessageDependencyException =
                new MessageDependencyException(failedMessageStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertMessageAsync(someMessage))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Message> addMessageTask =
                this.messageService.AddMessageAsync(someMessage);

            MessageDependencyException actualMessageDependencyException =
                await Assert.ThrowsAsync<MessageDependencyException>(() =>
                    addMessageTask.AsTask());

            // then
            actualMessageDependencyException.Should().BeEquivalentTo(
                expectedMessageDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertMessageAsync(someMessage),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedMessageDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfUnexpectedErrorOccursAndLogItAsync()
        {
            // given
            Message someMessage = CreateRandomMessage();
            var serviceException = new Exception();

            var failedMessageServiceException =
                new FailedMessageServiceException(serviceException);

            var expectedMessageServiceException =
                new MessageServiceException(failedMessageServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertMessageAsync(someMessage))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Message> addMessageTask =
                this.messageService.AddMessageAsync(someMessage);

            MessageServiceException actualMessageServiceException =
                await Assert.ThrowsAsync<MessageServiceException>(() =>
                    addMessageTask.AsTask());

            // then
            actualMessageServiceException.Should().BeEquivalentTo(
                expectedMessageServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertMessageAsync(someMessage),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedMessageServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
