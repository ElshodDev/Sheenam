//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.RentalContracts;
using Sheenam.Api.Models.Foundations.RentalContracts.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.RentalContracts
{
    public partial class RentalContractService
    {
        private delegate ValueTask<RentalContract> ReturningRentalContractFunction();
        private delegate IQueryable<RentalContract> ReturningRentalContractsFunction();

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
            catch (NotFoundRentalContractException notFoundRentalContractException)
            {
                throw CreateAndLogValidationException(notFoundRentalContractException);
            }
            catch (SqlException sqlException)
            {
                var failedRentalContractStorageException = new FailedRentalContractStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedRentalContractStorageException);
            }
            // SHU QISM QO'SHILDI
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsRentalContractException =
                    new AlreadyExistsRentalContractException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsRentalContractException);
            }
            catch (Exception exception)
            {
                var failedRentalContractServiceException = new FailedRentalContractServiceException(exception);
                throw CreateAndLogServiceException(failedRentalContractServiceException);
            }
        }

        private IQueryable<RentalContract> TryCatch(ReturningRentalContractsFunction returningRentalContractsFunction)
        {
            try
            {
                return returningRentalContractsFunction();
            }
            catch (SqlException sqlException)
            {
                var failedRentalContractStorageException = new FailedRentalContractStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedRentalContractStorageException);
            }
            catch (Exception exception)
            {
                var failedRentalContractServiceException = new FailedRentalContractServiceException(exception);
                throw CreateAndLogServiceException(failedRentalContractServiceException);
            }
        }

        private RentalContractValidationException CreateAndLogValidationException(Xeption exception)
        {
            var rentalContractValidationException = new RentalContractValidationException(exception);
            this.loggingBroker.LogError(rentalContractValidationException);
            return rentalContractValidationException;
        }

        private RentalContractDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var rentalContractDependencyException = new RentalContractDependencyException(exception);
            this.loggingBroker.LogCritical(rentalContractDependencyException);
            return rentalContractDependencyException;
        }

        private RentalContractDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var rentalContractDependencyValidationException = new RentalContractDependencyValidationException(exception);
            this.loggingBroker.LogError(rentalContractDependencyValidationException);
            return rentalContractDependencyValidationException;
        }

        private RentalContractServiceException CreateAndLogServiceException(Xeption exception)
        {
            var rentalContractServiceException = new RentalContractServiceException(exception);
            this.loggingBroker.LogError(rentalContractServiceException);
            return rentalContractServiceException;
        }
    }
}