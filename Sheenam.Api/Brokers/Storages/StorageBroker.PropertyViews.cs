//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.PropertyViews;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<PropertyView> PropertyViews { get; set; }

        public async ValueTask<PropertyView> InsertPropertyViewAsync(PropertyView propertyView)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<PropertyView> propertyViewEntityEntry =
                await broker.PropertyViews.AddAsync(propertyView);
            await broker.SaveChangesAsync();
            return propertyViewEntityEntry.Entity;
        }

        public IQueryable<PropertyView> SelectAllPropertyViews() =>
            SelectAll<PropertyView>();

        public async ValueTask<PropertyView> SelectPropertyViewByIdAsync(Guid propertyViewId)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.PropertyViews.FindAsync(propertyViewId);
        }

        public async ValueTask<PropertyView> DeletePropertyViewAsync(PropertyView propertyView)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<PropertyView> propertyViewEntityEntry =
                broker.PropertyViews.Remove(propertyView);
            await broker.SaveChangesAsync();
            return propertyViewEntityEntry.Entity;
        }
    }
}
