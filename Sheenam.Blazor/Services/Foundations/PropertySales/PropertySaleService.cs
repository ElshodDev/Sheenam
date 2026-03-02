//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.PropertySales;
namespace Sheenam.Blazor.Services.Foundations.PropertySales
{
    public partial class PropertySaleService : IPropertySaleService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public PropertySaleService(IApiBroker apiBroker, ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<PropertySale> AddPropertySaleAsync(PropertySale propertySale) =>
            await TryCatch(async () =>
            {
                ValidatePropertySaleOnAdd(propertySale);
                return await this.apiBroker.PostPropertySaleAsync(propertySale);
            });

        public async ValueTask<IQueryable<PropertySale>> RetrieveAllPropertySalesAsync() =>
            await TryCatch(async () =>
            {
                var propertySales = await this.apiBroker.GetAllPropertySalesAsync();
                return propertySales.AsQueryable();
            });

        public async ValueTask<PropertySale> RetrievePropertySaleByIdAsync(Guid propertySaleId) =>
            await TryCatch(async () =>
            {
                ValidatePropertySaleId(propertySaleId);
                return await this.apiBroker.GetPropertySaleByIdAsync(propertySaleId);
            });

        public async ValueTask<PropertySale> ModifyPropertySaleAsync(PropertySale propertySale) =>
            await TryCatch(async () =>
            {
                ValidatePropertySaleOnModify(propertySale);
                return await this.apiBroker.PutPropertySaleAsync(propertySale);
            });

        public async ValueTask<PropertySale> RemovePropertySaleByIdAsync(Guid propertySaleId) =>
            await TryCatch(async () =>
            {
                ValidatePropertySaleId(propertySaleId);
                return await this.apiBroker.DeletePropertySaleByIdAsync(propertySaleId);
            });
    }
}