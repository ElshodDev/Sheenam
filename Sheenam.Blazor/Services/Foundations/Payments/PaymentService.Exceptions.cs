//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Payments;
using Sheenam.Blazor.Models.Foundations.Payments.Exceptions;
using Xeptions;

namespace Sheenam.Blazor.Services.Foundations.Payments
{
    public partial class PaymentService
    {
        private delegate ValueTask<Payment> ReturningPaymentFunction();
        private delegate ValueTask<IQueryable<Payment>> ReturningPaymentsFunction();

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
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidPaymentReferenceException =
                    new InvalidPaymentReferenceException(httpResponseBadRequestException);

                throw CreateAndLogDependencyValidationException(invalidPaymentReferenceException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsPaymentException =
                    new AlreadyExistsPaymentException(httpResponseConflictException);

                throw CreateAndLogDependencyValidationException(alreadyExistsPaymentException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedPaymentDependencyException =
                    new FailedPaymentDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedPaymentDependencyException);
            }
            catch (Exception exception)
            {
                var failedPaymentServiceException =
                    new FailedPaymentServiceException(exception);

                throw CreateAndLogServiceException(failedPaymentServiceException);
            }
        }

        private async ValueTask<IQueryable<Payment>> TryCatch(ReturningPaymentsFunction returningPaymentsFunction)
        {
            try
            {
                return await returningPaymentsFunction();
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedPaymentDependencyException =
                    new FailedPaymentDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedPaymentDependencyException);
            }
            catch (Exception exception)
            {
                var failedPaymentServiceException =
                    new FailedPaymentServiceException(exception);

                throw CreateAndLogServiceException(failedPaymentServiceException);
            }
        }

        private PaymentValidationException CreateAndLogValidationException(Xeption exception)
        {
            var paymentValidationException = new PaymentValidationException(exception);
            this.loggingBroker.LogError(paymentValidationException);
            return paymentValidationException;
        }

        private PaymentDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var paymentDependencyValidationException = new PaymentDependencyValidationException(exception);
            this.loggingBroker.LogError(paymentDependencyValidationException);
            return paymentDependencyValidationException;
        }

        private PaymentDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var paymentDependencyException = new PaymentDependencyException(exception);
            this.loggingBroker.LogError(paymentDependencyException);
            return paymentDependencyException;
        }

        private PaymentServiceException CreateAndLogServiceException(Xeption exception)
        {
            var paymentServiceException = new PaymentServiceException(exception);
            this.loggingBroker.LogError(paymentServiceException);
            return paymentServiceException;
        }
    }
}