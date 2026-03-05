//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.Favorites;
using Sheenam.Api.Models.Foundations.Favorites.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.Favorites
{
    public partial class FavoriteService
    {
        private delegate IQueryable<Favorite> ReturningFavoritesFunction();
        private delegate ValueTask<Favorite> ReturningFavoriteFunction();

        private IQueryable<Favorite> TryCatch(ReturningFavoritesFunction returningFavoritesFunction)
        {
            try
            {
                return returningFavoritesFunction();
            }
            catch (SqlException sqlException)
            {
                var failedStorageException = new FailedFavoriteStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (Exception exception)
            {
                var failedServiceException = new FailedFavoriteServiceException(exception);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private async ValueTask<Favorite> TryCatch(ReturningFavoriteFunction returningFavoriteFunction)
        {
            try
            {
                return await returningFavoriteFunction();
            }
            catch (NullFavoriteException nullFavoriteException)
            {
                throw CreateAndLogValidationException(nullFavoriteException);
            }
            catch (InvalidFavoriteException invalidFavoriteException)
            {
                throw CreateAndLogValidationException(invalidFavoriteException);
            }
            catch (NotFoundFavoriteException notFoundFavoriteException)
            {
                throw CreateAndLogValidationException(notFoundFavoriteException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistException =
                    new AlreadyExistFavoriteException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistException);
            }
            catch (SqlException sqlException)
            {
                var failedStorageException = new FailedFavoriteStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (Exception exception)
            {
                var failedServiceException = new FailedFavoriteServiceException(exception);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private FavoriteValidationException CreateAndLogValidationException(Xeption exception)
        {
            var favoriteValidationException = new FavoriteValidationException(exception);
            this.loggingBroker.LogError(favoriteValidationException);
            return favoriteValidationException;
        }

        private FavoriteDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var favoriteDependencyException = new FavoriteDependencyException(exception);
            this.loggingBroker.LogCritical(favoriteDependencyException);
            return favoriteDependencyException;
        }

        private FavoriteDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var favoriteDependencyValidationException =
                new FavoriteDependencyValidationException(exception);
            this.loggingBroker.LogError(favoriteDependencyValidationException);
            return favoriteDependencyValidationException;
        }

        private FavoriteServiceException CreateAndLogServiceException(Xeption exception)
        {
            var favoriteServiceException = new FavoriteServiceException(exception);
            this.loggingBroker.LogError(favoriteServiceException);
            return favoriteServiceException;
        }
    }
}