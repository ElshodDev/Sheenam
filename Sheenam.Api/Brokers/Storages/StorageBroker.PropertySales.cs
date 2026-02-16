//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.PropertySales;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<PropertySale> PropertySales { get; set; }

        public async ValueTask<PropertySale> InsertPropertySaleAsync(PropertySale propertySale) =>
            await InsertAsync(propertySale);

        public IQueryable<PropertySale> SelectAllPropertySales() =>
                       SelectAll<PropertySale>();

        public async ValueTask<PropertySale> SelectPropertySaleByIdAsync(Guid propertySaleId) =>
                        await SelectAsync<PropertySale>(propertySaleId);

        public async ValueTask<PropertySale> UpdatePropertySaleAsync(PropertySale propertySale) =>
            await UpdateAsync(propertySale);
        public async ValueTask<PropertySale> DeletePropertySaleAsync(PropertySale propertySale) =>
                        await DeleteAsync(propertySale);
    }
}
