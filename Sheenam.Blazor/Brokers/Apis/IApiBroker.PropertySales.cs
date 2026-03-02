//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.PropertySales;
namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<PropertySale> PostPropertySaleAsync(PropertySale propertySale);
        ValueTask<List<PropertySale>> GetAllPropertySalesAsync();
        ValueTask<PropertySale> GetPropertySaleByIdAsync(Guid propertySaleId);
        ValueTask<PropertySale> PutPropertySaleAsync(PropertySale propertySale);
        ValueTask<PropertySale> DeletePropertySaleByIdAsync(Guid propertySaleId);
    }
}