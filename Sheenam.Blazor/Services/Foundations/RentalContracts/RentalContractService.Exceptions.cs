//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.RentalContracts;
using Sheenam.Blazor.Models.Foundations.RentalContracts.Exceptions;
using Xeptions;

namespace Sheenam.Blazor.Services.Foundations.RentalContracts
{
    public partial class RentalContractService
    {
        private delegate ValueTask<RentalContract> ReturningRentalContractFunction();
        private delegate ValueTask<IQueryable<RentalContract>> ReturningRentalContractsFunction();

        private async ValueTask<RentalContract> TryCatch(ReturningRentalContractFunction returningRentalContractFunction)
        {
            try
            {
                return await returningRentalContractFunction();
            }
            catch (NullRentalContractException nullRentalContractException)
            {
                throw CreateAndLogValidationException(nullRentalContractException);
            }
            catch (InvalidRentalContractException invalidRentalContractException)
            {
                throw CreateAndLogValidationException(invalidRentalContractException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidRentalContractReferenceException =
                    new InvalidRentalContractReferenceException(httpResponseBadRequestException);

                throw CreateAndLogDependencyValidationException(invalidRentalContractReferenceException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsRentalContractException =
                    new AlreadyExistsRentalContractException(httpResponseConflictException);

                throw CreateAndLogDependencyValidationException(alreadyExistsRentalContractException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedRentalContractDependencyException =
                    new FailedRentalContractDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedRentalContractDependencyException);
            }
            catch (Exception exception)
            {
                var failedRentalContractServiceException =
                    new FailedRentalContractServiceException(exception);

                throw CreateAndLogServiceException(failedRentalContractServiceException);
            }
        }

        private async ValueTask<IQueryable<RentalContract>> TryCatch(ReturningRentalContractsFunction returningRentalContractsFunction)
        {
            try
            {
                return await returningRentalContractsFunction();
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedRentalContractDependencyException =
                    new FailedRentalContractDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedRentalContractDependencyException);
            }
            catch (Exception exception)
            {
                var failedRentalContractServiceException =
                    new FailedRentalContractServiceException(exception);

                throw CreateAndLogServiceException(failedRentalContractServiceException);
            }
        }

        private RentalContractValidationException CreateAndLogValidationException(Xeption exception)
        {
            var rentalContractValidationException = new RentalContractValidationException(exception);
            this.loggingBroker.LogError(rentalContractValidationException);
            return rentalContractValidationException;
        }

        private RentalContractDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var rentalContractDependencyValidationException = new RentalContractDependencyValidationException(exception);
            this.loggingBroker.LogError(rentalContractDependencyValidationException);
            return rentalContractDependencyValidationException;
        }

        private RentalContractDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var rentalContractDependencyException = new RentalContractDependencyException(exception);
            this.loggingBroker.LogError(rentalContractDependencyException);
            return rentalContractDependencyException;
        }

        private RentalContractServiceException CreateAndLogServiceException(Xeption exception)
        {
            var rentalContractServiceException = new RentalContractServiceException(exception);
            this.loggingBroker.LogError(rentalContractServiceException);
            return rentalContractServiceException;
        }
    }
}