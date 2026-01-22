//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.PropertySales;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<PropertySale> PropertySales { get; set; }

        public async ValueTask<PropertySale> InsertPropertySaleAsync(PropertySale propertySale)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<PropertySale> propertySaleEntityEntry =
                await broker.PropertySales.AddAsync(propertySale);
            await broker.SaveChangesAsync();

            return propertySaleEntityEntry.Entity;
        }

        public IQueryable<PropertySale> SelectAllPropertySales()
        {
            using var broker = new StorageBroker(this.configuration);
            return broker.PropertySales.AsQueryable();
        }

        public async ValueTask<PropertySale> SelectPropertySaleByIdAsync(Guid propertySaleId)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.PropertySales.FindAsync(propertySaleId);
        }

        public async ValueTask<PropertySale> UpdatePropertySaleAsync(PropertySale propertySale)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<PropertySale> propertySaleEntityEntry =
                broker.PropertySales.Update(propertySale);
            await broker.SaveChangesAsync();

            return propertySaleEntityEntry.Entity;
        }

        public async ValueTask<PropertySale> DeletePropertySaleAsync(PropertySale propertySale)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<PropertySale> propertySaleEntityEntry =
                broker.PropertySales.Remove(propertySale);
            await broker.SaveChangesAsync();

            return propertySaleEntityEntry.Entity;
        }
    }
}
