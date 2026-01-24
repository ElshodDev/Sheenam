//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.SaleOffers;
using Sheenam.Api.Models.Foundations.SaleOffers.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.SaleOffers
{
    public partial class SaleOfferService
    {
        private async ValueTask<SaleOffer> TryCatch(ReturningSaleOfferFunction returningSaleOfferFunction)
        {
            try
            {
                return await returningSaleOfferFunction();
            }
            catch (NullSaleOfferException nullSaleOfferException)
            {
                throw CreateAndLogValidationException(nullSaleOfferException);
            }
            catch (InvalidSaleOfferException invalidSaleOfferException)
            {
                throw CreateAndLogValidationException(invalidSaleOfferException);
            }
            catch (NotFoundSaleOfferException notFoundSaleOfferException)
            {
                throw CreateAndLogValidationException(notFoundSaleOfferException);
            }
            catch (SqlException sqlException)
            {
                var failedSaleOfferStorageException =
                    new FailedSaleOfferStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedSaleOfferStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsSaleOfferException =
                    new AlreadyExistsSaleOfferException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsSaleOfferException);
            }
            catch (Exception exception)
            {
                var failedSaleOfferServiceException =
                    new FailedSaleOfferServiceException(exception);

                throw CreateAndLogServiceException(failedSaleOfferServiceException);
            }
        }

        private IQueryable<SaleOffer> TryCatch(ReturningSaleOffersFunction returningSaleOffersFunction)
        {
            try
            {
                return returningSaleOffersFunction();
            }
            catch (SqlException sqlException)
            {
                var failedSaleOfferStorageException =
                    new FailedSaleOfferStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedSaleOfferStorageException);
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

        private SaleOfferDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var saleOfferDependencyValidationException =
                new SaleOfferDependencyValidationException(exception);

            this.loggingBroker.LogError(saleOfferDependencyValidationException);

            return saleOfferDependencyValidationException;
        }

        private SaleOfferDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var saleOfferDependencyException = new SaleOfferDependencyException(exception);
            this.loggingBroker.LogCritical(saleOfferDependencyException);

            return saleOfferDependencyException;
        }

        private SaleOfferServiceException CreateAndLogServiceException(Xeption exception)
        {
            var saleOfferServiceException = new SaleOfferServiceException(exception);
            this.loggingBroker.LogError(saleOfferServiceException);

            return saleOfferServiceException;
        }

        private delegate ValueTask<SaleOffer> ReturningSaleOfferFunction();
        private delegate IQueryable<SaleOffer> ReturningSaleOffersFunction();
    }
}