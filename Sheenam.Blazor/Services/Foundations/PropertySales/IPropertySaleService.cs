//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.PropertySales;
namespace Sheenam.Blazor.Services.Foundations.PropertySales
{
    public interface IPropertySaleService
    {
        ValueTask<PropertySale> AddPropertySaleAsync(PropertySale propertySale);
        ValueTask<IQueryable<PropertySale>> RetrieveAllPropertySalesAsync();
        ValueTask<PropertySale> RetrievePropertySaleByIdAsync(Guid propertySaleId);
        ValueTask<PropertySale> ModifyPropertySaleAsync(PropertySale propertySale);
        ValueTask<PropertySale> RemovePropertySaleByIdAsync(Guid propertySaleId);
    }
}