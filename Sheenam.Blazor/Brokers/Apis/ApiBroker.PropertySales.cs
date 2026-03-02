//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.PropertySales;
namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string PropertySalesRelativeUrl = "api/propertysales";

        public async ValueTask<PropertySale> PostPropertySaleAsync(PropertySale propertySale) =>
            await PostAsync(PropertySalesRelativeUrl, propertySale);

        public async ValueTask<List<PropertySale>> GetAllPropertySalesAsync() =>
            await GetAsync<List<PropertySale>>(PropertySalesRelativeUrl);

        public async ValueTask<PropertySale> GetPropertySaleByIdAsync(Guid propertySaleId) =>
            await GetAsync<PropertySale>($"{PropertySalesRelativeUrl}/{propertySaleId}");

        public async ValueTask<PropertySale> PutPropertySaleAsync(PropertySale propertySale) =>
            await PutAsync($"{PropertySalesRelativeUrl}/{propertySale.Id}", propertySale);

        public async ValueTask<PropertySale> DeletePropertySaleByIdAsync(Guid propertySaleId) =>
            await DeleteAsync<PropertySale>($"{PropertySalesRelativeUrl}/{propertySaleId}");
    }
}