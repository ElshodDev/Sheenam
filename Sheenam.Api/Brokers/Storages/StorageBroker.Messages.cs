//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.Messages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Message> Messages { get; set; }

        public async ValueTask<Message> InsertMessageAsync(Message message)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Message> messageEntityEntry =
                await broker.Messages.AddAsync(message);
            await broker.SaveChangesAsync();
            return messageEntityEntry.Entity;
        }

        public IQueryable<Message> SelectAllMessages() =>
            SelectAll<Message>();

        public async ValueTask<Message> SelectMessageByIdAsync(Guid messageId)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.Messages.FindAsync(messageId);
        }

        public async ValueTask<Message> UpdateMessageAsync(Message message)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Message> messageEntityEntry =
                broker.Messages.Update(message);
            await broker.SaveChangesAsync();
            return messageEntityEntry.Entity;
        }

        public async ValueTask<Message> DeleteMessageAsync(Message message)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Message> messageEntityEntry =
                broker.Messages.Remove(message);
            await broker.SaveChangesAsync();
            return messageEntityEntry.Entity;
        }
    }
}
