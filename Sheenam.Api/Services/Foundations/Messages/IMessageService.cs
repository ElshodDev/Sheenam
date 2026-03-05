//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Messages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Messages
{
    public interface IMessageService
    {
        ValueTask<Message> AddMessageAsync(Message message);
        IQueryable<Message> RetrieveAllMessages();
        IQueryable<Message> RetrieveMessagesByUserId(Guid userId);
        ValueTask<Message> RetrieveMessageByIdAsync(Guid messageId);
        ValueTask<Message> ModifyMessageAsync(Message message);
        ValueTask<Message> RemoveMessageByIdAsync(Guid messageId);
    }
}
