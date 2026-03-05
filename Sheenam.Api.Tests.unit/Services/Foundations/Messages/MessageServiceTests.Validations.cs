//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.Messages;
using Sheenam.Api.Models.Foundations.Messages.Exceptions;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Messages
{
    public partial class MessageServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfMessageIsNullAndLogItAsync()
        {
            // given
            Message nullMessage = null;
            var nullMessageException = new NullMessageException();

            var expectedMessageValidationException =
                new MessageValidationException(nullMessageException);

            // when
            ValueTask<Message> addMessageTask =
                this.messageService.AddMessageAsync(nullMessage);

            // then
            await Assert.ThrowsAsync<MessageValidationException>(() =>
                addMessageTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedMessageValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfMessageIsInvalidAndLogItAsync()
        {
            // given
            var invalidMessage = new Message
            {
                Id = Guid.Empty,
                SenderId = Guid.Empty,
                ReceiverId = Guid.Empty,
                Content = string.Empty,
                SentDate = default
            };

            var invalidMessageException = new InvalidMessageException();

            invalidMessageException.AddData(
                key: nameof(Message.Id),
                values: "Id is required");

            invalidMessageException.AddData(
                key: nameof(Message.SenderId),
                values: "Id is required");

            invalidMessageException.AddData(
                key: nameof(Message.ReceiverId),
                values: "Id is required");

            invalidMessageException.AddData(
                key: nameof(Message.Content),
                values: "Text is required");

            invalidMessageException.AddData(
                key: nameof(Message.SentDate),
                values: "Date is required");

            var expectedMessageValidationException =
                new MessageValidationException(invalidMessageException);

            // when
            ValueTask<Message> addMessageTask =
                this.messageService.AddMessageAsync(invalidMessage);

            // then
            await Assert.ThrowsAsync<MessageValidationException>(() =>
                addMessageTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedMessageValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertMessageAsync(It.IsAny<Message>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidMessageId = Guid.Empty;
            var invalidMessageException = new InvalidMessageException();

            invalidMessageException.AddData(
                key: nameof(Message.Id),
                values: "Id is required");

            var expectedMessageValidationException =
                new MessageValidationException(invalidMessageException);

            // when
            ValueTask<Message> retrieveMessageTask =
                this.messageService.RetrieveMessageByIdAsync(invalidMessageId);

            // then
            await Assert.ThrowsAsync<MessageValidationException>(() =>
                retrieveMessageTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedMessageValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectMessageByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfNotFoundAndLogItAsync()
        {
            // given
            Guid someMessageId = Guid.NewGuid();
            Message noMessage = null;

            var notFoundMessageException =
                new NotFoundMessageException(someMessageId);

            var expectedMessageValidationException =
                new MessageValidationException(notFoundMessageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectMessageByIdAsync(someMessageId))
                    .ReturnsAsync(noMessage);

            // when
            ValueTask<Message> retrieveMessageTask =
                this.messageService.RetrieveMessageByIdAsync(someMessageId);

            // then
            await Assert.ThrowsAsync<MessageValidationException>(() =>
                retrieveMessageTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectMessageByIdAsync(someMessageId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedMessageValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
