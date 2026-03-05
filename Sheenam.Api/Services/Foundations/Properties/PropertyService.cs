//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Properties;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Properties
{
    public partial class PropertyService : IPropertyService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public PropertyService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Property> AddPropertyAsync(Property property) =>
            TryCatch(async () =>
            {
                ValidatePropertyOnAdd(property);
                return await this.storageBroker.InsertPropertyAsync(property);
            });

        public IQueryable<Property> RetrieveAllProperties() =>
            TryCatch(() => this.storageBroker.SelectAllProperties());

        public ValueTask<Property> RetrievePropertyByIdAsync(Guid propertyId) =>
            TryCatch(async () =>
            {
                ValidatePropertyId(propertyId);
                Property maybeProperty =
                    await this.storageBroker.SelectPropertyByIdAsync(propertyId);
                ValidateStorageProperty(maybeProperty, propertyId);
                return maybeProperty;
            });

        public ValueTask<Property> ModifyPropertyAsync(Property property) =>
            TryCatch(async () =>
            {
                ValidatePropertyOnModify(property);
                Property maybeProperty =
                    await this.storageBroker.SelectPropertyByIdAsync(property.Id);
                ValidateStorageProperty(maybeProperty, property.Id);
                return await this.storageBroker.UpdatePropertyAsync(property);
            });

        public ValueTask<Property> RemovePropertyByIdAsync(Guid propertyId) =>
            TryCatch(async () =>
            {
                ValidatePropertyId(propertyId);
                Property maybeProperty =
                    await this.storageBroker.SelectPropertyByIdAsync(propertyId);
                ValidateStorageProperty(maybeProperty, propertyId);
                return await this.storageBroker.DeletePropertyAsync(maybeProperty);
            });
    }
}