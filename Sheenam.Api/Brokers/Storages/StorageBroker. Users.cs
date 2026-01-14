//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<User> Users { get; set; }

        public async ValueTask<User> InsertUserAsync(User user)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<User> userEntityEntry = await broker.Users.AddAsync(user);
            await broker.SaveChangesAsync();

            return userEntityEntry.Entity;
        }

        public IQueryable<User> SelectAllUsers()
        {
            using var broker = new StorageBroker(this.configuration);

            return broker.Users.AsQueryable();
        }

        public async ValueTask<User> SelectUserByIdAsync(Guid userId)
        {
            using var broker = new StorageBroker(this.configuration);

            return await broker.Users.FindAsync(userId);
        }

        public async ValueTask<User> SelectUserByEmailAsync(string email)
        {
            using var broker = new StorageBroker(this.configuration);

            return await broker.Users
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public async ValueTask<User> UpdateUserAsync(User user)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<User> userEntityEntry = broker.Users.Update(user);
            await broker.SaveChangesAsync();

            return userEntityEntry.Entity;
        }

        public async ValueTask<User> DeleteUserAsync(User user)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<User> userEntityEntry = broker.Users.Remove(user);
            await broker.SaveChangesAsync();

            return userEntityEntry.Entity;
        }
    }
}