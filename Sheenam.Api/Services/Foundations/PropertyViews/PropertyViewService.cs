//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.PropertyViews;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.PropertyViews
{
    public partial class PropertyViewService : IPropertyViewService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public PropertyViewService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<PropertyView> AddPropertyViewAsync(PropertyView propertyView) =>
            TryCatch(async () =>
            {
                ValidatePropertyViewOnAdd(propertyView);
                return await this.storageBroker.InsertPropertyViewAsync(propertyView);
            });

        public IQueryable<PropertyView> RetrieveAllPropertyViews() =>
            TryCatch(() => this.storageBroker.SelectAllPropertyViews());

        public IQueryable<PropertyView> RetrievePropertyViewsByPropertyId(Guid propertyId) =>
            TryCatch(() => this.storageBroker
                .SelectAllPropertyViews()
                .Where(pv => pv.PropertyId == propertyId));

        public ValueTask<PropertyView> RetrievePropertyViewByIdAsync(Guid propertyViewId) =>
            TryCatch(async () =>
            {
                ValidatePropertyViewId(propertyViewId);
                PropertyView maybePropertyView =
                    await this.storageBroker.SelectPropertyViewByIdAsync(propertyViewId);
                ValidateStoragePropertyView(maybePropertyView, propertyViewId);
                return maybePropertyView;
            });

        public ValueTask<PropertyView> RemovePropertyViewByIdAsync(Guid propertyViewId) =>
            TryCatch(async () =>
            {
                ValidatePropertyViewId(propertyViewId);
                PropertyView maybePropertyView =
                    await this.storageBroker.SelectPropertyViewByIdAsync(propertyViewId);
                ValidateStoragePropertyView(maybePropertyView, propertyViewId);
                return await this.storageBroker.DeletePropertyViewAsync(maybePropertyView);
            });
    }
}
