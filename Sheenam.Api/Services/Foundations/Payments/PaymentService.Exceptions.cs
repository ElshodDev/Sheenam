//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.Payments;
using Sheenam.Api.Models.Foundations.Payments.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.Payments
{
    public partial class PaymentService
    {
        private delegate ValueTask<Payment> ReturningPaymentFunction();
        private delegate IQueryable<Payment> ReturningPaymentsFunction();

        private async ValueTask<Payment> TryCatch(ReturningPaymentFunction returningPaymentFunction)
        {
            try
            {
                return await returningPaymentFunction();
            }
            catch (NullPaymentException nullPaymentException)
            {
                throw CreateAndLogValidationException(nullPaymentException);
            }
            catch (InvalidPaymentException invalidPaymentException)
            {
                throw CreateAndLogValidationException(invalidPaymentException);
            }
            catch (SqlException sqlException)
            {
                var failedPaymentStorageException = new FailedPaymentStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedPaymentStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsPaymentException = new AlreadyExistsPaymentException(duplicateKeyException);
                throw CreateAndLogDependencyValidationException(alreadyExistsPaymentException);
            }
            catch (Exception exception)
            {
                var failedPaymentServiceException = new FailedPaymentServiceException(exception);
                throw CreateAndLogServiceException(failedPaymentServiceException);
            }
        }

        private PaymentValidationException CreateAndLogValidationException(Xeption exception)
        {
            var paymentValidationException = new PaymentValidationException(exception);
            this.loggingBroker.LogError(paymentValidationException);
            return paymentValidationException;
        }

        private PaymentDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var paymentDependencyException = new PaymentDependencyException(exception);
            this.loggingBroker.LogCritical(paymentDependencyException);
            return paymentDependencyException;
        }

        private PaymentDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var paymentDependencyValidationException = new PaymentDependencyValidationException(exception);
            this.loggingBroker.LogError(paymentDependencyValidationException);
            return paymentDependencyValidationException;
        }

        private PaymentServiceException CreateAndLogServiceException(Xeption exception)
        {
            var paymentServiceException = new PaymentServiceException(exception);
            this.loggingBroker.LogError(paymentServiceException);
            return paymentServiceException;
        }
    }
}