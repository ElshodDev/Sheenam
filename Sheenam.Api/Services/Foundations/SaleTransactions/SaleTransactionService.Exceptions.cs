//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.SaleTransactions;
using Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.SaleTransactions
{
    public partial class SaleTransactionService
    {
        private delegate ValueTask<SaleTransaction> ReturningSaleTransactionFunction();
        private delegate IQueryable<SaleTransaction> ReturningSaleTransactionsFunction();

        private async ValueTask<SaleTransaction> TryCatch(ReturningSaleTransactionFunction returningSaleTransactionFunction)
        {
            try
            {
                return await returningSaleTransactionFunction();
            }
            catch (NullSaleTransactionException nullSaleTransactionException)
            {
                throw CreateAndLogValidationException(nullSaleTransactionException);
            }
            catch (InvalidSaleTransactionException invalidSaleTransactionException)
            {
                throw CreateAndLogValidationException(invalidSaleTransactionException);
            }
            catch (NotFoundSaleTransactionException notFoundSaleTransactionException)
            {
                throw CreateAndLogValidationException(notFoundSaleTransactionException);
            }
            catch (SqlException sqlException)
            {
                var failedSaleTransactionStorageException = new FailedSaleTransactionStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedSaleTransactionStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsSaleTransactionException = new AlreadyExistsSaleTransactionException(duplicateKeyException);
                throw CreateAndLogDependencyValidationException(alreadyExistsSaleTransactionException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedSaleTransactionException = new LockedSaleTransactionException(dbUpdateConcurrencyException);
                throw CreateAndLogDependencyValidationException(lockedSaleTransactionException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedSaleTransactionStorageException = new FailedSaleTransactionStorageException(dbUpdateException);
                throw CreateAndLogDependencyException(failedSaleTransactionStorageException);
            }
            catch (Exception exception)
            {
                var failedSaleTransactionServiceException = new FailedSaleTransactionServiceException(exception);
                throw CreateAndLogServiceException(failedSaleTransactionServiceException);
            }
        }

        private IQueryable<SaleTransaction> TryCatch(ReturningSaleTransactionsFunction returningSaleTransactionsFunction)
        {
            try
            {
                return returningSaleTransactionsFunction();
            }
            catch (SqlException sqlException)
            {
                var failedSaleTransactionStorageException = new FailedSaleTransactionStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedSaleTransactionStorageException);
            }
            catch (Exception exception)
            {
                var failedSaleTransactionServiceException = new FailedSaleTransactionServiceException(exception);
                throw CreateAndLogServiceException(failedSaleTransactionServiceException);
            }
        }

        private SaleTransactionValidationException CreateAndLogValidationException(Xeption exception)
        {
            var saleTransactionValidationException = new SaleTransactionValidationException(exception);
            this.loggingBroker.LogError(saleTransactionValidationException);

            return saleTransactionValidationException;
        }

        private SaleTransactionDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var saleTransactionDependencyException = new SaleTransactionDependencyException(exception);
            this.loggingBroker.LogCritical(saleTransactionDependencyException);

            return saleTransactionDependencyException;
        }

        private SaleTransactionDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var saleTransactionDependencyValidationException = new SaleTransactionDependencyValidationException(exception);
            this.loggingBroker.LogError(saleTransactionDependencyValidationException);

            return saleTransactionDependencyValidationException;
        }

        private SaleTransactionDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var saleTransactionDependencyException = new SaleTransactionDependencyException(exception);
            this.loggingBroker.LogError(saleTransactionDependencyException);

            return saleTransactionDependencyException;
        }

        private SaleTransactionServiceException CreateAndLogServiceException(Xeption exception)
        {
            var saleTransactionServiceException = new SaleTransactionServiceException(exception);
            this.loggingBroker.LogError(saleTransactionServiceException);

            return saleTransactionServiceException;
        }
    }
}