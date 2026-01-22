//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Api.Models.Foundations.PropertySales;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.PropertySales
{
    public interface IPropertySaleService
    {
        ValueTask<PropertySale> AddPropertySaleAsync(PropertySale propertySale);
        IQueryable<PropertySale> RetrieveAllPropertySales();
        ValueTask<PropertySale> RetrievePropertySaleByIdAsync(Guid id);
        ValueTask<PropertySale> ModifyPropertySaleAsync(PropertySale propertySale);
        ValueTask<PropertySale> RemovePropertySaleByIdAsync(Guid propertySaleId);
    }
}