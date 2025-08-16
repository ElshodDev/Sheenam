//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Npgsql;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.Guests
{
    public partial class GuestService
    {
        private delegate ValueTask<Guest> ReturningGuestFunction();

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
            catch (PostgresException postgresException) 
            {
               var failedGuestStorageException=new
                    FailedGuestStorageException(postgresException);

                throw CreateAndLogCriticalDependencyException(failedGuestStorageException);
            }
            catch(DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistGuestException =
                   new AlreadyExistGuestException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistGuestException);
            }
        }
        private GuestValidationException CreateAndLogValidationException(Xeption exception)
        {
            var guestValidationException =
                new GuestValidationException(exception);

            this.loggingBroker.LogError(guestValidationException);

            return guestValidationException;
        }
        private GuestDependecyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var guestDependencyException=
                new GuestDependecyException(exception);

            this.loggingBroker.LogCritical(guestDependencyException);

            return guestDependencyException;
        }
        private GuestDependecyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var guestDependecyValidationException =
                new GuestDependecyValidationException(exception);

            this.loggingBroker.LogError(guestDependecyValidationException);

            return guestDependecyValidationException;
        }
    }
}
