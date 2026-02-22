//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Models.Foundations.Guests.Exceptions;
using Xeptions;

namespace Sheenam.Blazor.Services.Foundations.Guests
{
    public partial class GuestService
    {
        private delegate ValueTask<Guest> ReturningGuestFunction();
        private delegate ValueTask<IQueryable<Guest>> ReturningGuestsFunction();

        private async ValueTask<Guest> TryCatch(ReturningGuestFunction returningGuestFunction)
        {
            try
            {
                return await returningGuestFunction();
            }
            catch (NullGuestException nullGuestException)
            {
                throw CreateAndLogValidationException(nullGuestException);
            }
            catch (InvalidGuestException invalidGuestException)
            {
                throw CreateAndLogValidationException(invalidGuestException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidGuestReferenceException =
                    new InvalidGuestReferenceException(httpResponseBadRequestException);

                throw CreateAndLogDependencyValidationException(invalidGuestReferenceException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsGuestException =
                    new AlreadyExistsGuestException(httpResponseConflictException);

                throw CreateAndLogDependencyValidationException(alreadyExistsGuestException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedGuestDependencyException =
                    new FailedGuestDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedGuestDependencyException);
            }
            catch (Exception exception)
            {
                var failedGuestServiceException =
                    new FailedGuestServiceException(exception);

                throw CreateAndLogServiceException(failedGuestServiceException);
            }
        }
        private async ValueTask<IQueryable<Guest>> TryCatch(ReturningGuestsFunction returningGuestsFunction)
        {
            try
            {
                return await returningGuestsFunction();
            }
            catch (AggregateException aggregateException)
                when (aggregateException.InnerException is HttpResponseException httpResponseException)
            {
                var failedGuestDependencyException =
                    new FailedGuestDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedGuestDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedGuestDependencyException =
                    new FailedGuestDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedGuestDependencyException);
            }
            catch (Exception exception)
            {
                var failedGuestServiceException =
                    new FailedGuestServiceException(exception);

                throw CreateAndLogServiceException(failedGuestServiceException);
            }
        }

        private GuestValidationException CreateAndLogValidationException(Xeption exception)
        {
            var guestValidationException = new GuestValidationException(exception);
            this.loggingBroker.LogError(guestValidationException);
            return guestValidationException;
        }

        private GuestDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var guestDependencyValidationException = new GuestDependencyValidationException(exception);
            this.loggingBroker.LogError(guestDependencyValidationException);
            return guestDependencyValidationException;
        }

        private GuestDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var guestDependencyException = new GuestDependencyException(exception);
            this.loggingBroker.LogError(guestDependencyException);
            return guestDependencyException;
        }

        private GuestServiceException CreateAndLogServiceException(Xeption exception)
        {
            var guestServiceException = new GuestServiceException(exception);
            this.loggingBroker.LogError(guestServiceException);
            return guestServiceException;
        }
    }
}