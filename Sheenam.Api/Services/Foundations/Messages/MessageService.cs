//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Messages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Messages
{
    public partial class MessageService : IMessageService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public MessageService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Message> AddMessageAsync(Message message) =>
            TryCatch(async () =>
            {
                ValidateMessageOnAdd(message);
                return await this.storageBroker.InsertMessageAsync(message);
            });

        public IQueryable<Message> RetrieveAllMessages() =>
            TryCatch(() => this.storageBroker.SelectAllMessages());

        public IQueryable<Message> RetrieveMessagesByUserId(Guid userId) =>
            TryCatch(() => this.storageBroker
                .SelectAllMessages()
                .Where(m => m.SenderId == userId || m.ReceiverId == userId));

        public ValueTask<Message> RetrieveMessageByIdAsync(Guid messageId) =>
            TryCatch(async () =>
            {
                ValidateMessageId(messageId);
                Message maybeMessage =
                    await this.storageBroker.SelectMessageByIdAsync(messageId);
                ValidateStorageMessage(maybeMessage, messageId);
                return maybeMessage;
            });

        public ValueTask<Message> ModifyMessageAsync(Message message) =>
            TryCatch(async () =>
            {
                ValidateMessageOnModify(message);
                Message maybeMessage =
                    await this.storageBroker.SelectMessageByIdAsync(message.Id);
                ValidateStorageMessage(maybeMessage, message.Id);
                return await this.storageBroker.UpdateMessageAsync(message);
            });

        public ValueTask<Message> RemoveMessageByIdAsync(Guid messageId) =>
            TryCatch(async () =>
            {
                ValidateMessageId(messageId);
                Message maybeMessage =
                    await this.storageBroker.SelectMessageByIdAsync(messageId);
                ValidateStorageMessage(maybeMessage, messageId);
                return await this.storageBroker.DeleteMessageAsync(maybeMessage);
            });
    }
}
