//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Guests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Guest> Guests { get; set; }
        public async ValueTask<Guest> InserGuestAsync(Guest guest)
        {
            using var broker = new StorageBroker(this.configuration);

            var guestEntityEntry = await broker.Guests.AddAsync(guest);
            await broker.SaveChangesAsync();

            return guestEntityEntry.Entity;
        }
        public IQueryable<Guest> RetrieveGuestAsync()
        {
            using var broker = new StorageBroker(this.configuration);

            return broker.Guests.AsQueryable();
        }

        public async ValueTask<Guest> RetrieveGuestByIdAsync(Guid Id)
        {
            using var broker = new StorageBroker(this.configuration);

            return await broker.Guests.FindAsync(Id);
        }
    }
}
