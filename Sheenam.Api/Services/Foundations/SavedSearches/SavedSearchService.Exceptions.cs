//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.SavedSearches;
using Sheenam.Api.Models.Foundations.SavedSearches.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.SavedSearches
{
    public partial class SavedSearchService
    {
        private delegate IQueryable<SavedSearch> ReturningSearchesFunction();
        private delegate ValueTask<SavedSearch> ReturningSavedSearchFunction();

        private IQueryable<SavedSearch> TryCatch(ReturningSearchesFunction returningSearchesFunction)
        {
            try
            {
                return returningSearchesFunction();
            }
            catch (SqlException sqlException)
            {
                var failedStorageException =
                    new FailedSavedSearchStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (Exception exception)
            {
                var failedServiceException =
                    new FailedSavedSearchServiceException(exception);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private async ValueTask<SavedSearch> TryCatch(
            ReturningSavedSearchFunction returningSavedSearchFunction)
        {
            try
            {
                return await returningSavedSearchFunction();
            }
            catch (NullSavedSearchException nullSavedSearchException)
            {
                throw CreateAndLogValidationException(nullSavedSearchException);
            }
            catch (InvalidSavedSearchException invalidSavedSearchException)
            {
                throw CreateAndLogValidationException(invalidSavedSearchException);
            }
            catch (NotFoundSavedSearchException notFoundSavedSearchException)
            {
                throw CreateAndLogValidationException(notFoundSavedSearchException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsException =
                    new AlreadyExistsSavedSearchException(duplicateKeyException);
                throw CreateAndLogDependencyValidationException(alreadyExistsException);
            }
            catch (SqlException sqlException)
            {
                var failedStorageException =
                    new FailedSavedSearchStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (Exception exception)
            {
                var failedServiceException =
                    new FailedSavedSearchServiceException(exception);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private SavedSearchValidationException CreateAndLogValidationException(Xeption exception)
        {
            var savedSearchValidationException = new SavedSearchValidationException(exception);
            this.loggingBroker.LogError(savedSearchValidationException);
            return savedSearchValidationException;
        }

        private SavedSearchDependencyException CreateAndLogCriticalDependencyException(
            Xeption exception)
        {
            var savedSearchDependencyException = new SavedSearchDependencyException(exception);
            this.loggingBroker.LogCritical(savedSearchDependencyException);
            return savedSearchDependencyException;
        }

        private SavedSearchDependencyValidationException
            CreateAndLogDependencyValidationException(Xeption exception)
        {
            var savedSearchDependencyValidationException =
                new SavedSearchDependencyValidationException(exception);
            this.loggingBroker.LogError(savedSearchDependencyValidationException);
            return savedSearchDependencyValidationException;
        }

        private SavedSearchServiceException CreateAndLogServiceException(Xeption exception)
        {
            var savedSearchServiceException = new SavedSearchServiceException(exception);
            this.loggingBroker.LogError(savedSearchServiceException);
            return savedSearchServiceException;
        }
    }
}
