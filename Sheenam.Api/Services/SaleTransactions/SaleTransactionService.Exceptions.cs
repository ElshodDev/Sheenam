//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.SaleTransactions;
using Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.SaleTransactions
{
    public partial class SaleTransactionService
    {
        public delegate ValueTask<SaleTransaction> ReturningSaleTransactionFunction();
        public delegate IQueryable<SaleTransaction> ReturningSaleTransactionsFunction();

        public async ValueTask<SaleTransaction> TryCatch(ReturningSaleTransactionFunction returningSaleTransactionFunction)
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
            catch (SqlException sqlServerException)
            {
                var failedStorageException =
                    new FailedSaleTransactionStorageException(sqlServerException);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsException =
                    new AlreadyExistsSaleTransactionException(duplicateKeyException);
                throw CreateAndLogDependencyValidationException(alreadyExistsException);
            }
            catch (Exception exception)
            {
                var failedSaleTransactionServiceException =
                    new FailedSaleTransactionServiceException(exception);
                throw CreateAndLogServiceException(failedSaleTransactionServiceException);
            }
        }

        private SaleTransactionValidationException CreateAndLogValidationException(Xeption exception)
        {
            var saleTransactionValidationException =
                new SaleTransactionValidationException(exception);
            this.loggingBroker.LogError(saleTransactionValidationException);

            return saleTransactionValidationException;
        }

        private SaleTransactionDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var saleTransactionDependencyException =
                new SaleTransactionDependencyException(exception);
            this.loggingBroker.LogError(saleTransactionDependencyException);
            return saleTransactionDependencyException;
        }
        private SaleTransactionDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var saleTransactionDependencyException =
                new SaleTransactionDependencyException(exception);
            this.loggingBroker.LogCritical(saleTransactionDependencyException);
            return saleTransactionDependencyException;
        }
        private SaleTransactionServiceException CreateAndLogServiceException(Xeption servivceException)
        {
            var saleTransactionServiceException =
                new SaleTransactionServiceException(servivceException);
            this.loggingBroker.LogError(saleTransactionServiceException);
            return saleTransactionServiceException;
        }
        private SaleTransactionDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var saleTransactionDependencyValidationException =
                new SaleTransactionDependencyValidationException(exception);
            this.loggingBroker.LogError(saleTransactionDependencyValidationException);
            return saleTransactionDependencyValidationException;
        }
    }
}
