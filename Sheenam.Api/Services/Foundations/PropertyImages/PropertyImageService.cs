//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.PropertyImages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.PropertyImages
{
    public partial class PropertyImageService : IPropertyImageService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public PropertyImageService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<PropertyImage> AddPropertyImageAsync(PropertyImage propertyImage) =>
            TryCatch(async () =>
            {
                ValidatePropertyImageOnAdd(propertyImage);
                return await this.storageBroker.InsertPropertyImageAsync(propertyImage);
            });

        public IQueryable<PropertyImage> RetrieveAllPropertyImages() =>
            TryCatch(() => this.storageBroker.SelectAllPropertyImages());

        public IQueryable<PropertyImage> RetrievePropertyImagesByPropertyId(Guid propertyId) =>
            TryCatch(() => this.storageBroker
                .SelectAllPropertyImages()
                .Where(pi => pi.PropertyId == propertyId));

        public ValueTask<PropertyImage> RetrievePropertyImageByIdAsync(Guid propertyImageId) =>
            TryCatch(async () =>
            {
                ValidatePropertyImageId(propertyImageId);
                PropertyImage maybePropertyImage =
                    await this.storageBroker.SelectPropertyImageByIdAsync(propertyImageId);
                ValidateStoragePropertyImage(maybePropertyImage, propertyImageId);
                return maybePropertyImage;
            });

        public ValueTask<PropertyImage> ModifyPropertyImageAsync(PropertyImage propertyImage) =>
            TryCatch(async () =>
            {
                ValidatePropertyImageOnModify(propertyImage);
                PropertyImage maybePropertyImage =
                    await this.storageBroker.SelectPropertyImageByIdAsync(propertyImage.Id);
                ValidateStoragePropertyImage(maybePropertyImage, propertyImage.Id);
                return await this.storageBroker.UpdatePropertyImageAsync(propertyImage);
            });

        public ValueTask<PropertyImage> RemovePropertyImageByIdAsync(Guid propertyImageId) =>
            TryCatch(async () =>
            {
                ValidatePropertyImageId(propertyImageId);
                PropertyImage maybePropertyImage =
                    await this.storageBroker.SelectPropertyImageByIdAsync(propertyImageId);
                ValidateStoragePropertyImage(maybePropertyImage, propertyImageId);
                return await this.storageBroker.DeletePropertyImageAsync(maybePropertyImage);
            });
    }
}
