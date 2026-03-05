//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Messages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Message> InsertMessageAsync(Message message);
        IQueryable<Message> SelectAllMessages();
        ValueTask<Message> SelectMessageByIdAsync(Guid messageId);
        ValueTask<Message> UpdateMessageAsync(Message message);
        ValueTask<Message> DeleteMessageAsync(Message message);
    }
}
