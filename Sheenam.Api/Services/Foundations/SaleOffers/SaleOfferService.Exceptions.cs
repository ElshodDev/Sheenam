//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
        private delegate ValueTask<SaleOffer> ReturningSaleOfferFunction();
        private delegate IQueryable<SaleOffer> ReturningSaleOffersFunction();

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
                var failedSaleOfferStorageException = new FailedSaleOfferStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedSaleOfferStorageException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedSaleOfferException = new LockedSaleOfferException(dbUpdateConcurrencyException);
                throw CreateAndLogDependencyValidationException(lockedSaleOfferException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedSaleOfferStorageException = new FailedSaleOfferStorageException(dbUpdateException);
                throw CreateAndLogDependencyException(failedSaleOfferStorageException);
            }
            catch (Exception exception)
            {
                var failedSaleOfferServiceException = new FailedSaleOfferServiceException(exception);
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
                var failedSaleOfferStorageException = new FailedSaleOfferStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedSaleOfferStorageException);
            }
            catch (Exception exception)
            {
                var failedSaleOfferServiceException = new FailedSaleOfferServiceException(exception);
                throw CreateAndLogServiceException(failedSaleOfferServiceException);
            }
        }

        private SaleOfferValidationException CreateAndLogValidationException(Xeption exception)
        {
            var saleOfferValidationException = new SaleOfferValidationException(exception);
            this.loggingBroker.LogError(saleOfferValidationException);

            return saleOfferValidationException;
        }

        private SaleOfferDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var saleOfferDependencyException = new SaleOfferDependencyException(exception);
            this.loggingBroker.LogCritical(saleOfferDependencyException);

            return saleOfferDependencyException;
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