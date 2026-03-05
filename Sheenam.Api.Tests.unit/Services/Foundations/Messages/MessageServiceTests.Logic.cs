//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Messages;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Messages
{
    public partial class MessageServiceTests
    {
        [Fact]
        public async Task ShouldAddMessageAsync()
        {
            // given
            Message randomMessage = CreateRandomMessage();
            Message inputMessage = randomMessage;
            Message storageMessage = inputMessage;
            Message expectedMessage = storageMessage.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertMessageAsync(inputMessage))
                    .ReturnsAsync(storageMessage);

            // when
            Message actualMessage =
                await this.messageService.AddMessageAsync(inputMessage);

            // then
            actualMessage.Should().BeEquivalentTo(expectedMessage);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertMessageAsync(inputMessage),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveMessageByIdAsync()
        {
            // given
            Guid randomMessageId = Guid.NewGuid();
            Message randomMessage = CreateRandomMessage();
            Message storageMessage = randomMessage;
            Message expectedMessage = storageMessage.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectMessageByIdAsync(randomMessageId))
                    .ReturnsAsync(storageMessage);

            // when
            Message actualMessage =
                await this.messageService.RetrieveMessageByIdAsync(randomMessageId);

            // then
            actualMessage.Should().BeEquivalentTo(expectedMessage);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectMessageByIdAsync(randomMessageId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyMessageAsync()
        {
            // given
            Message randomMessage = CreateRandomMessage();
            Message inputMessage = randomMessage;
            Message storageMessage = inputMessage.DeepClone();
            Message updatedMessage = inputMessage;
            Message expectedMessage = updatedMessage.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectMessageByIdAsync(inputMessage.Id))
                    .ReturnsAsync(storageMessage);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateMessageAsync(inputMessage))
                    .ReturnsAsync(updatedMessage);

            // when
            Message actualMessage =
                await this.messageService.ModifyMessageAsync(inputMessage);

            // then
            actualMessage.Should().BeEquivalentTo(expectedMessage);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectMessageByIdAsync(inputMessage.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateMessageAsync(inputMessage),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveMessageByIdAsync()
        {
            // given
            Guid randomMessageId = Guid.NewGuid();
            Message randomMessage = CreateRandomMessage();
            Message storageMessage = randomMessage;
            Message expectedMessage = storageMessage.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectMessageByIdAsync(randomMessageId))
                    .ReturnsAsync(storageMessage);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteMessageAsync(storageMessage))
                    .ReturnsAsync(expectedMessage);

            // when
            Message actualMessage =
                await this.messageService.RemoveMessageByIdAsync(randomMessageId);

            // then
            actualMessage.Should().BeEquivalentTo(expectedMessage);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectMessageByIdAsync(randomMessageId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteMessageAsync(storageMessage),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
