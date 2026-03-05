//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.Properties;
using Sheenam.Api.Models.Foundations.Properties.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.Properties
{
    public partial class PropertyService
    {
        private delegate IQueryable<Property> ReturningPropertiesFunction();
        private delegate ValueTask<Property> ReturningPropertyFunction();

        private IQueryable<Property> TryCatch(ReturningPropertiesFunction returningPropertiesFunction)
        {
            try
            {
                return returningPropertiesFunction();
            }
            catch (SqlException sqlException)
            {
                var failedStorageException = new FailedPropertyStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (Exception exception)
            {
                var failedServiceException = new FailedPropertyServiceException(exception);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private async ValueTask<Property> TryCatch(ReturningPropertyFunction returningPropertyFunction)
        {
            try
            {
                return await returningPropertyFunction();
            }
            catch (NullPropertyException nullPropertyException)
            {
                throw CreateAndLogValidationException(nullPropertyException);
            }
            catch (InvalidPropertyException invalidPropertyException)
            {
                throw CreateAndLogValidationException(invalidPropertyException);
            }
            catch (NotFoundPropertyException notFoundPropertyException)
            {
                throw CreateAndLogValidationException(notFoundPropertyException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistException = new AlreadyExistPropertyException(duplicateKeyException);
                throw CreateAndLogDependencyValidationException(alreadyExistException);
            }
            catch (SqlException sqlException)
            {
                var failedStorageException = new FailedPropertyStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (Exception exception)
            {
                var failedServiceException = new FailedPropertyServiceException(exception);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private PropertyValidationException CreateAndLogValidationException(Xeption exception)
        {
            var propertyValidationException = new PropertyValidationException(exception);
            this.loggingBroker.LogError(propertyValidationException);
            return propertyValidationException;
        }

        private PropertyDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var propertyDependencyException = new PropertyDependencyException(exception);
            this.loggingBroker.LogCritical(propertyDependencyException);
            return propertyDependencyException;
        }

        private PropertyDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var propertyDependencyValidationException =
                new PropertyDependencyValidationException(exception);
            this.loggingBroker.LogError(propertyDependencyValidationException);
            return propertyDependencyValidationException;
        }

        private PropertyServiceException CreateAndLogServiceException(Xeption exception)
        {
            var propertyServiceException = new PropertyServiceException(exception);
            this.loggingBroker.LogError(propertyServiceException);
            return propertyServiceException;
        }
    }
}