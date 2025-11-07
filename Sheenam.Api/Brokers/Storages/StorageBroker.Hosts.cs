//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.Hosts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Host> Hosts { get; set; }


        public async ValueTask<Host> InsertHostAsync(Host host)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Host> hostEntityEntry =
                await broker.Hosts.AddAsync(host);
            await broker.SaveChangesAsync();
            return hostEntityEntry.Entity;
        }
        public IQueryable<Host> SelectAllHosts() =>
            this.Hosts;

        public async ValueTask<Host> SelectHostByIdAsync(Guid hostId)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.Hosts.FindAsync(hostId);
        }
        public async ValueTask<Host> UpdateHostAsync(Host host)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Host> hostEntityEntry =
                broker.Hosts.Update(host);

            await broker.SaveChangesAsync();

            return hostEntityEntry.Entity;
        }
    }
}
