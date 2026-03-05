//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.PropertyViews;
using Sheenam.Api.Models.Foundations.PropertyViews.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.PropertyViews
{
    public partial class PropertyViewService
    {
        private delegate IQueryable<PropertyView> ReturningPropertyViewsFunction();
        private delegate ValueTask<PropertyView> ReturningPropertyViewFunction();

        private IQueryable<PropertyView> TryCatch(
            ReturningPropertyViewsFunction returningPropertyViewsFunction)
        {
            try
            {
                return returningPropertyViewsFunction();
            }
            catch (SqlException sqlException)
            {
                var failedStorageException =
                    new FailedPropertyViewStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (Exception exception)
            {
                var failedServiceException =
                    new FailedPropertyViewServiceException(exception);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private async ValueTask<PropertyView> TryCatch(
            ReturningPropertyViewFunction returningPropertyViewFunction)
        {
            try
            {
                return await returningPropertyViewFunction();
            }
            catch (NullPropertyViewException nullPropertyViewException)
            {
                throw CreateAndLogValidationException(nullPropertyViewException);
            }
            catch (InvalidPropertyViewException invalidPropertyViewException)
            {
                throw CreateAndLogValidationException(invalidPropertyViewException);
            }
            catch (NotFoundPropertyViewException notFoundPropertyViewException)
            {
                throw CreateAndLogValidationException(notFoundPropertyViewException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsException =
                    new AlreadyExistsPropertyViewException(duplicateKeyException);
                throw CreateAndLogDependencyValidationException(alreadyExistsException);
            }
            catch (SqlException sqlException)
            {
                var failedStorageException =
                    new FailedPropertyViewStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (Exception exception)
            {
                var failedServiceException =
                    new FailedPropertyViewServiceException(exception);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private PropertyViewValidationException CreateAndLogValidationException(Xeption exception)
        {
            var propertyViewValidationException = new PropertyViewValidationException(exception);
            this.loggingBroker.LogError(propertyViewValidationException);
            return propertyViewValidationException;
        }

        private PropertyViewDependencyException CreateAndLogCriticalDependencyException(
            Xeption exception)
        {
            var propertyViewDependencyException = new PropertyViewDependencyException(exception);
            this.loggingBroker.LogCritical(propertyViewDependencyException);
            return propertyViewDependencyException;
        }

        private PropertyViewDependencyValidationException
            CreateAndLogDependencyValidationException(Xeption exception)
        {
            var propertyViewDependencyValidationException =
                new PropertyViewDependencyValidationException(exception);
            this.loggingBroker.LogError(propertyViewDependencyValidationException);
            return propertyViewDependencyValidationException;
        }

        private PropertyViewServiceException CreateAndLogServiceException(Xeption exception)
        {
            var propertyViewServiceException = new PropertyViewServiceException(exception);
            this.loggingBroker.LogError(propertyViewServiceException);
            return propertyViewServiceException;
        }
    }
}
