//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Api.Models.Foundations.PropertySales;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<PropertySale> InsertPropertySaleAsync(PropertySale propertySale);
        ValueTask<PropertySale> SelectPropertySaleByIdAsync(Guid id);
        IQueryable<PropertySale> SelectAllPropertySales();
        ValueTask<PropertySale> UpdatePropertySaleAsync(PropertySale propertySale);
        ValueTask<PropertySale> DeletePropertySaleAsync(PropertySale propertySale);
    }
}