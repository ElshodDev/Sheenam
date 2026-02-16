//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.PropertySales;
using Sheenam.Api.Models.Foundations.PropertySales.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.PropertySales
{
    public partial class PropertySaleService
    {
        private delegate ValueTask<PropertySale> ReturningPropertySaleFunction();
        private delegate IQueryable<PropertySale> ReturningPropertySalesFunction();

        private async ValueTask<PropertySale> TryCatch(ReturningPropertySaleFunction returningPropertySaleFunction)
        {
            try
            {
                return await returningPropertySaleFunction();
            }
            catch (NullPropertySaleException nullPropertySaleException)
            {
                throw CreateAndLogValidationException(nullPropertySaleException);
            }
            catch (InvalidPropertySaleException invalidPropertySaleException)
            {
                throw CreateAndLogValidationException(invalidPropertySaleException);
            }
            catch (NotFoundPropertySaleException notFoundPropertySaleException)
            {
                throw CreateAndLogValidationException(notFoundPropertySaleException);
            }
            catch (DuplicateKeyException duplicateKeyException) // Xatolik tuzatildi
            {
                var alreadyExistPropertySaleException = new AlreadyExistPropertySaleException(duplicateKeyException);
                throw CreateAndLogDependencyValidationException(alreadyExistPropertySaleException);
            }
            catch (SqlException sqlException)
            {
                var failedPropertySaleStorageException = new FailedPropertySaleStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedPropertySaleStorageException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedPropertySaleException = new LockedPropertySaleException(dbUpdateConcurrencyException);
                throw CreateAndLogDependencyValidationException(lockedPropertySaleException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedPropertySaleStorageException = new FailedPropertySaleStorageException(dbUpdateException);
                throw CreateAndLogDependencyException(failedPropertySaleStorageException);
            }
            catch (Exception exception)
            {
                var failedPropertySaleServiceException = new FailedPropertySaleServiceException(exception);
                throw CreateAndLogServiceException(failedPropertySaleServiceException);
            }
        }

        private IQueryable<PropertySale> TryCatch(ReturningPropertySalesFunction returningPropertySalesFunction)
        {
            try
            {
                return returningPropertySalesFunction();
            }
            catch (SqlException sqlException)
            {
                var failedPropertySaleStorageException = new FailedPropertySaleStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedPropertySaleStorageException);
            }
            catch (Exception exception)
            {
                var failedPropertySaleServiceException = new FailedPropertySaleServiceException(exception);
                throw CreateAndLogServiceException(failedPropertySaleServiceException);
            }
        }

        private PropertySaleValidationException CreateAndLogValidationException(Xeption exception)
        {
            var propertySaleValidationException = new PropertySaleValidationException(exception);
            this.loggingBroker.LogError(propertySaleValidationException);
            return propertySaleValidationException;
        }

        private PropertySaleDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var propertySaleDependencyException = new PropertySaleDependencyException(exception);
            this.loggingBroker.LogCritical(propertySaleDependencyException);
            return propertySaleDependencyException;
        }

        private PropertySaleDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var propertySaleDependencyValidationException = new PropertySaleDependencyValidationException(exception);
            this.loggingBroker.LogError(propertySaleDependencyValidationException);
            return propertySaleDependencyValidationException;
        }

        private PropertySaleDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var propertySaleDependencyException = new PropertySaleDependencyException(exception);
            this.loggingBroker.LogError(propertySaleDependencyException);
            return propertySaleDependencyException;
        }

        private PropertySaleServiceException CreateAndLogServiceException(Xeption exception)
        {
            var propertySaleServiceException = new PropertySaleServiceException(exception);
            this.loggingBroker.LogError(propertySaleServiceException);
            return propertySaleServiceException;
        }
    }
}