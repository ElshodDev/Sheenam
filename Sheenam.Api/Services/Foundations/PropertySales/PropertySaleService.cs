using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.PropertySales;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.PropertySales
{
    public partial class PropertySaleService : IPropertySaleService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public PropertySaleService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<PropertySale> AddPropertySaleAsync(PropertySale propertySale) =>
        TryCatch(async () =>
        {
            ValidatePropertySaleOnAdd(propertySale);

            return await this.storageBroker.InsertPropertySaleAsync(propertySale);
        });

        public IQueryable<PropertySale> RetrieveAllPropertySales() =>
            TryCatch(() => this.storageBroker.SelectAllPropertySales());

        public ValueTask<PropertySale> RetrievePropertySaleByIdAsync(Guid propertySaleId) =>
        TryCatch(async () =>
        {
            ValidatePropertySaleId(propertySaleId);

            PropertySale maybePropertySale =
                await this.storageBroker.SelectPropertySaleByIdAsync(propertySaleId);

            ValidateStoragePropertySale(maybePropertySale, propertySaleId);

            return maybePropertySale;
        });

        public ValueTask<PropertySale> ModifyPropertySaleAsync(PropertySale propertySale) =>
        TryCatch(async () =>
        {
            ValidatePropertySaleOnModify(propertySale);

            PropertySale maybePropertySale =
                await this.storageBroker.SelectPropertySaleByIdAsync(propertySale.Id);

            ValidateStoragePropertySale(maybePropertySale, propertySale.Id);

            return await this.storageBroker.UpdatePropertySaleAsync(propertySale);
        });

        public ValueTask<PropertySale> RemovePropertySaleByIdAsync(Guid propertySaleId) =>
        TryCatch(async () =>
        {
            ValidatePropertySaleId(propertySaleId);

            PropertySale maybePropertySale =
                await this.storageBroker.SelectPropertySaleByIdAsync(propertySaleId);

            ValidateStoragePropertySale(maybePropertySale, propertySaleId);

            // FIX: Bu yerda DeleteHomeRequestAsync emas, DeletePropertySaleAsync bo'lishi kerak
            return await this.storageBroker.DeletePropertySaleAsync(maybePropertySale);
        });

        // Boshqa metodlar uchun placeholderlar (Interfeysga moslab nomlandi)
        public ValueTask<PropertySale> ApprovePropertySaleAsync(Guid propertySaleId) =>
            throw new NotImplementedException();

        public ValueTask<PropertySale> RejectPropertySaleAsync(Guid propertySaleId, string rejectionReason = null) =>
            throw new NotImplementedException();

        public ValueTask<PropertySale> CancelPropertySaleAsync(Guid propertySaleId) =>
            throw new NotImplementedException();

        public IQueryable<PropertySale> RetrievePropertySalesByStatusAsync(PropertySaleStatus status) =>
            throw new NotImplementedException();
    }
}