//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.PropertySales;
using Sheenam.Blazor.Models.Foundations.PropertySales.Exceptions;
using Xeptions;
namespace Sheenam.Blazor.Services.Foundations.PropertySales
{
    public partial class PropertySaleService
    {
        private delegate ValueTask<PropertySale> ReturningPropertySaleFunction();
        private delegate ValueTask<IQueryable<PropertySale>> ReturningPropertySalesFunction();

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
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidPropertySaleReferenceException =
                    new InvalidPropertySaleReferenceException(httpResponseBadRequestException);
                throw CreateAndLogDependencyValidationException(invalidPropertySaleReferenceException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsPropertySaleException =
                    new AlreadyExistsPropertySaleException(httpResponseConflictException);
                throw CreateAndLogDependencyValidationException(alreadyExistsPropertySaleException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedPropertySaleDependencyException =
                    new FailedPropertySaleDependencyException(httpResponseException);
                throw CreateAndLogDependencyException(failedPropertySaleDependencyException);
            }
            catch (Exception exception)
            {
                var failedPropertySaleServiceException =
                    new FailedPropertySaleServiceException(exception);
                throw CreateAndLogServiceException(failedPropertySaleServiceException);
            }
        }

        private async ValueTask<IQueryable<PropertySale>> TryCatch(ReturningPropertySalesFunction returningPropertySalesFunction)
        {
            try
            {
                return await returningPropertySalesFunction();
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedPropertySaleDependencyException =
                    new FailedPropertySaleDependencyException(httpResponseException);
                throw CreateAndLogDependencyException(failedPropertySaleDependencyException);
            }
            catch (Exception exception)
            {
                var failedPropertySaleServiceException =
                    new FailedPropertySaleServiceException(exception);
                throw CreateAndLogServiceException(failedPropertySaleServiceException);
            }
        }

        private PropertySaleValidationException CreateAndLogValidationException(Xeption exception)
        {
            var propertySaleValidationException = new PropertySaleValidationException(exception);
            this.loggingBroker.LogError(propertySaleValidationException);
            return propertySaleValidationException;
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