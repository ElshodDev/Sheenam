//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.PropertyImages;
using Sheenam.Api.Models.Foundations.PropertyImages.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.PropertyImages
{
    public partial class PropertyImageService
    {
        private delegate IQueryable<PropertyImage> ReturningPropertyImagesFunction();
        private delegate ValueTask<PropertyImage> ReturningPropertyImageFunction();

        private IQueryable<PropertyImage> TryCatch(
            ReturningPropertyImagesFunction returningPropertyImagesFunction)
        {
            try
            {
                return returningPropertyImagesFunction();
            }
            catch (SqlException sqlException)
            {
                var failedStorageException =
                    new FailedPropertyImageStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (Exception exception)
            {
                var failedServiceException =
                    new FailedPropertyImageServiceException(exception);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private async ValueTask<PropertyImage> TryCatch(
            ReturningPropertyImageFunction returningPropertyImageFunction)
        {
            try
            {
                return await returningPropertyImageFunction();
            }
            catch (NullPropertyImageException nullPropertyImageException)
            {
                throw CreateAndLogValidationException(nullPropertyImageException);
            }
            catch (InvalidPropertyImageException invalidPropertyImageException)
            {
                throw CreateAndLogValidationException(invalidPropertyImageException);
            }
            catch (NotFoundPropertyImageException notFoundPropertyImageException)
            {
                throw CreateAndLogValidationException(notFoundPropertyImageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsException =
                    new AlreadyExistsPropertyImageException(duplicateKeyException);
                throw CreateAndLogDependencyValidationException(alreadyExistsException);
            }
            catch (SqlException sqlException)
            {
                var failedStorageException =
                    new FailedPropertyImageStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (Exception exception)
            {
                var failedServiceException =
                    new FailedPropertyImageServiceException(exception);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private PropertyImageValidationException CreateAndLogValidationException(Xeption exception)
        {
            var propertyImageValidationException = new PropertyImageValidationException(exception);
            this.loggingBroker.LogError(propertyImageValidationException);
            return propertyImageValidationException;
        }

        private PropertyImageDependencyException CreateAndLogCriticalDependencyException(
            Xeption exception)
        {
            var propertyImageDependencyException = new PropertyImageDependencyException(exception);
            this.loggingBroker.LogCritical(propertyImageDependencyException);
            return propertyImageDependencyException;
        }

        private PropertyImageDependencyValidationException
            CreateAndLogDependencyValidationException(Xeption exception)
        {
            var propertyImageDependencyValidationException =
                new PropertyImageDependencyValidationException(exception);
            this.loggingBroker.LogError(propertyImageDependencyValidationException);
            return propertyImageDependencyValidationException;
        }

        private PropertyImageServiceException CreateAndLogServiceException(Xeption exception)
        {
            var propertyImageServiceException = new PropertyImageServiceException(exception);
            this.loggingBroker.LogError(propertyImageServiceException);
            return propertyImageServiceException;
        }
    }
}
