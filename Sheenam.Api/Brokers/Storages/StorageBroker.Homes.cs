//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.Homes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Home> Homes { get; set; }

        public async ValueTask<Home> InsertHomeAsync(Home home)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Home> homeEntityEntry =
                await broker.Homes.AddAsync(home);

            await broker.SaveChangesAsync();
            return homeEntityEntry.Entity;
        }

        public IQueryable<Home> SelectAllHomes() =>
            this.Homes;

        public async ValueTask<Home> SelectHomeByIdAsync(Guid homeId)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.Homes.FindAsync(homeId);
        }

        public async ValueTask<Home> UpdateHomeAsync(Home home)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Home> homeEntityEntry =
                broker.Homes.Update(home);
            await broker.SaveChangesAsync();
            return homeEntityEntry.Entity;
        }

        public async ValueTask<Home> DeleteHomeAsync(Home home)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Home> homeEntityEntry =
                broker.Homes.Remove(home);
            await broker.SaveChangesAsync();
            return homeEntityEntry.Entity;
        }

        public async ValueTask<Home> DeleteHomeByIdAsync(Guid homeId)
        {
            Home maybeHome = await this.SelectHomeByIdAsync(homeId);

            if (maybeHome is null)
                return null;

            this.Entry(maybeHome).State = EntityState.Deleted;
            await this.SaveChangesAsync();

            return maybeHome;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Home>()
                .Property(h => h.Type)
                .HasConversion<string>();

            ConfigureUsers(modelBuilder);
        }
    }
}