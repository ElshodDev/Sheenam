//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.Data.SqlClient;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.PropertySales;
using Sheenam.Api.Models.Foundations.PropertySales.Exceptions;
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

        public ValueTask<PropertySale> AddPropertySaleAsync(PropertySale propertySale)
        {
            return TryCatch(async () =>
            {
                ValidatePropertySaleOnAdd(propertySale);

                return await this.storageBroker.InsertPropertySaleAsync(propertySale);
            });
        }

        public IQueryable<PropertySale> RetrieveAllPropertySales()
        {
            return this.storageBroker.SelectAllPropertySales();
        }

        public async ValueTask<PropertySale> RetrievePropertySaleByIdAsync(Guid id)
        {
            PropertySale maybePropertySale =
                await this.storageBroker.SelectPropertySaleByIdAsync(id);

            if (maybePropertySale is null)
            {
                throw new NotFoundPropertySaleException(id);
            }

            return maybePropertySale;
        }

        public async ValueTask<PropertySale> ModifyPropertySaleAsync(PropertySale propertySale)
        {
            ValidatePropertySaleOnModify(propertySale);

            PropertySale trackedPropertySale =
                await this.storageBroker.SelectPropertySaleByIdAsync(propertySale.Id);

            ValidateStoragePropertySale(trackedPropertySale, propertySale.Id);

            trackedPropertySale.HostId = propertySale.HostId;
            trackedPropertySale.Address = propertySale.Address;
            trackedPropertySale.Description = propertySale.Description;
            trackedPropertySale.Type = propertySale.Type;
            trackedPropertySale.NumberOfBedrooms = propertySale.NumberOfBedrooms;
            trackedPropertySale.NumberOfBathrooms = propertySale.NumberOfBathrooms;
            trackedPropertySale.Area = propertySale.Area;
            trackedPropertySale.SalePrice = propertySale.SalePrice;
            trackedPropertySale.Status = propertySale.Status;
            trackedPropertySale.ImageUrls = propertySale.ImageUrls;
            trackedPropertySale.LegalDocuments = propertySale.LegalDocuments;
            trackedPropertySale.IsFeatured = propertySale.IsFeatured;
            trackedPropertySale.UpdatedDate = DateTimeOffset.UtcNow;

            return await this.storageBroker.UpdatePropertySaleAsync(trackedPropertySale);
        }

        public async ValueTask<PropertySale> RemovePropertySaleByIdAsync(Guid propertySaleId)
        {
            try
            {
                PropertySale maybePropertySale =
                    await this.storageBroker.SelectPropertySaleByIdAsync(propertySaleId);

                if (maybePropertySale == null)
                {
                    throw new NotFoundPropertySaleException(propertySaleId);
                }

                PropertySale deletedPropertySale =
                    await this.storageBroker.DeletePropertySaleAsync(maybePropertySale);

                return deletedPropertySale;
            }
            catch (NotFoundPropertySaleException notFoundPropertySaleException)
            {
                throw new PropertySaleValidationException(notFoundPropertySaleException);
            }
            catch (SqlException sqlException)
            {
                throw new PropertySaleDependencyException(
                    new FailedPropertySaleStorageException(sqlException));
            }
            catch (Exception exception)
            {
                throw new PropertySaleServiceException(
                    new FailedPropertySaleServiceException(exception));
            }
        }

        private void ValidatePropertySaleOnModify(PropertySale propertySale)
        {
            if (propertySale == null)
                throw new PropertySaleValidationException("PropertySale cannot be null.");

            if (string.IsNullOrWhiteSpace(propertySale.Address))
                throw new PropertySaleValidationException("PropertySale address is required.");

            if (propertySale.SalePrice <= 0)
                throw new PropertySaleValidationException("PropertySale price must be greater than zero.");
        }

        private void ValidateStoragePropertySale(PropertySale maybePropertySale, Guid id)
        {
            if (maybePropertySale == null)
                throw new NotFoundPropertySaleException($"PropertySale with id {id} not found.");
        }
    }
}
