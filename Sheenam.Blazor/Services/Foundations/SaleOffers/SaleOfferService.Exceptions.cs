//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.SaleOffers;
using Sheenam.Blazor.Models.Foundations.SaleOffers.Exceptions;
using Xeptions;
namespace Sheenam.Blazor.Services.Foundations.SaleOffers
{
    public partial class SaleOfferService
    {
        private delegate ValueTask<SaleOffer> ReturningSaleOfferFunction();
        private delegate ValueTask<IQueryable<SaleOffer>> ReturningSaleOffersFunction();

        private async ValueTask<SaleOffer> TryCatch(ReturningSaleOfferFunction retSaleOfferFunction)
        {
            try
            {
                return await retSaleOfferFunction();
            }
            catch (NullSaleOfferException nullSaleOfferException)
            {
                throw CreateAndLogValidationException(nullSaleOfferException);
            }
            catch (InvalidSaleOfferException invalidSaleOfferException)
            {
                throw CreateAndLogValidationException(invalidSaleOfferException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidSaleOfferReferenceException =
                    new InvalidSaleOfferReferenceException(httpResponseBadRequestException);
                throw CreateAndLogDependencyValidationException(invalidSaleOfferReferenceException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsSaleOfferException =
                    new AlreadyExistsSaleOfferException(httpResponseConflictException);
                throw CreateAndLogDependencyValidationException(alreadyExistsSaleOfferException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedSaleOfferDependencyException =
                    new FailedSaleOfferDependencyException(httpResponseException);
                throw CreateAndLogDependencyException(failedSaleOfferDependencyException);
            }
            catch (Exception exception)
            {
                var failedSaleOfferServiceException =
                    new FailedSaleOfferServiceException(exception);
                throw CreateAndLogServiceException(failedSaleOfferServiceException);
            }
        }

        private async ValueTask<IQueryable<SaleOffer>> TryCatch(ReturningSaleOffersFunction retSaleOffersFunction)
        {
            try
            {
                return await retSaleOffersFunction();
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedSaleOfferDependencyException =
                    new FailedSaleOfferDependencyException(httpResponseException);
                throw CreateAndLogDependencyException(failedSaleOfferDependencyException);
            }
            catch (Exception exception)
            {
                var failedSaleOfferServiceException =
                    new FailedSaleOfferServiceException(exception);
                throw CreateAndLogServiceException(failedSaleOfferServiceException);
            }
        }

        private SaleOfferValidationException CreateAndLogValidationException(Xeption exception)
        {
            var saleOfferValidationException = new SaleOfferValidationException(exception);
            this.loggingBroker.LogError(saleOfferValidationException);
            return saleOfferValidationException;
        }

        private SaleOfferDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var saleOfferDependencyValidationException = new SaleOfferDependencyValidationException(exception);
            this.loggingBroker.LogError(saleOfferDependencyValidationException);
            return saleOfferDependencyValidationException;
        }

        private SaleOfferDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var saleOfferDependencyException = new SaleOfferDependencyException(exception);
            this.loggingBroker.LogError(saleOfferDependencyException);
            return saleOfferDependencyException;
        }

        private SaleOfferServiceException CreateAndLogServiceException(Xeption exception)
        {
            var saleOfferServiceException = new SaleOfferServiceException(exception);
            this.loggingBroker.LogError(saleOfferServiceException);
            return saleOfferServiceException;
        }
    }
}