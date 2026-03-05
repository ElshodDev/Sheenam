//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.PropertyImages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<PropertyImage> PropertyImages { get; set; }

        public async ValueTask<PropertyImage> InsertPropertyImageAsync(PropertyImage propertyImage)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<PropertyImage> propertyImageEntityEntry =
                await broker.PropertyImages.AddAsync(propertyImage);
            await broker.SaveChangesAsync();
            return propertyImageEntityEntry.Entity;
        }

        public IQueryable<PropertyImage> SelectAllPropertyImages() =>
            SelectAll<PropertyImage>();

        public async ValueTask<PropertyImage> SelectPropertyImageByIdAsync(Guid propertyImageId)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.PropertyImages.FindAsync(propertyImageId);
        }

        public async ValueTask<PropertyImage> UpdatePropertyImageAsync(PropertyImage propertyImage)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<PropertyImage> propertyImageEntityEntry =
                broker.PropertyImages.Update(propertyImage);
            await broker.SaveChangesAsync();
            return propertyImageEntityEntry.Entity;
        }

        public async ValueTask<PropertyImage> DeletePropertyImageAsync(PropertyImage propertyImage)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<PropertyImage> propertyImageEntityEntry =
                broker.PropertyImages.Remove(propertyImage);
            await broker.SaveChangesAsync();
            return propertyImageEntityEntry.Entity;
        }
    }
}
