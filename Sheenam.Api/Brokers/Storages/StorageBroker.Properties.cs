//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.Properties;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Property> Properties { get; set; }

        public async ValueTask<Property> InsertPropertyAsync(Property property)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Property> propertyEntityEntry =
                await broker.Properties.AddAsync(property);
            await broker.SaveChangesAsync();
            return propertyEntityEntry.Entity;
        }

        public IQueryable<Property> SelectAllProperties() =>
            SelectAll<Property>();

        public async ValueTask<Property> SelectPropertyByIdAsync(Guid propertyId)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.Properties.FindAsync(propertyId);
        }

        public async ValueTask<Property> UpdatePropertyAsync(Property property)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Property> propertyEntityEntry =
                broker.Properties.Update(property);
            await broker.SaveChangesAsync();
            return propertyEntityEntry.Entity;
        }

        public async ValueTask<Property> DeletePropertyAsync(Property property)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Property> propertyEntityEntry =
                broker.Properties.Remove(property);
            await broker.SaveChangesAsync();
            return propertyEntityEntry.Entity;
        }
    }
}