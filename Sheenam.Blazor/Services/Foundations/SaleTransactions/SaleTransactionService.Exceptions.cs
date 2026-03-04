//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.SaleTransactions;
using Sheenam.Blazor.Models.Foundations.SaleTransactions.Exceptions;
using Xeptions;
namespace Sheenam.Blazor.Services.Foundations.SaleTransactions
{
    public partial class SaleTransactionService
    {
        private delegate ValueTask<SaleTransaction> ReturningSaleTransactionFunction();
        private delegate ValueTask<IQueryable<SaleTransaction>> ReturningSaleTransactionsFunction();

        private async ValueTask<SaleTransaction> TryCatch(ReturningSaleTransactionFunction function)
        {
            try
            {
                return await function();
            }
            catch (NullSaleTransactionException nullSaleTransactionException)
            {
                throw CreateAndLogValidationException(nullSaleTransactionException);
            }
            catch (InvalidSaleTransactionException invalidSaleTransactionException)
            {
                throw CreateAndLogValidationException(invalidSaleTransactionException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidSaleTransactionReferenceException =
                    new InvalidSaleTransactionReferenceException(httpResponseBadRequestException);
                throw CreateAndLogDependencyValidationException(invalidSaleTransactionReferenceException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsSaleTransactionException =
                    new AlreadyExistsSaleTransactionException(httpResponseConflictException);
                throw CreateAndLogDependencyValidationException(alreadyExistsSaleTransactionException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedSaleTransactionDependencyException =
                    new FailedSaleTransactionDependencyException(httpResponseException);
                throw CreateAndLogDependencyException(failedSaleTransactionDependencyException);
            }
            catch (Exception exception)
            {
                var failedSaleTransactionServiceException =
                    new FailedSaleTransactionServiceException(exception);
                throw CreateAndLogServiceException(failedSaleTransactionServiceException);
            }
        }

        private async ValueTask<IQueryable<SaleTransaction>> TryCatch(ReturningSaleTransactionsFunction function)
        {
            try
            {
                return await function();
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedSaleTransactionDependencyException =
                    new FailedSaleTransactionDependencyException(httpResponseException);
                throw CreateAndLogDependencyException(failedSaleTransactionDependencyException);
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
            var saleTransactionValidationException = new SaleTransactionValidationException(exception);
            this.loggingBroker.LogError(saleTransactionValidationException);
            return saleTransactionValidationException;
        }

        private SaleTransactionDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var saleTransactionDependencyValidationException =
                new SaleTransactionDependencyValidationException(exception);
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