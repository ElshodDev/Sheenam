//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Reviews;
using Sheenam.Api.Models.Foundations.Reviews.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.Reviews
{
    public partial class ReviewService
    {
        private delegate ValueTask<Review> ReturningReviewFunction();
        private delegate IQueryable<Review> ReturningReviewsFunction();

        private async ValueTask<Review> TryCatch(ReturningReviewFunction returningReviewFunction)
        {
            try
            {
                return await returningReviewFunction();
            }
            catch (NullReviewException nullReviewException)
            {
                throw CreateAndLogValidationException(nullReviewException);
            }
            catch (InvalidReviewException invalidReviewException)
            {
                throw CreateAndLogValidationException(invalidReviewException);
            }
            catch (NotFoundReviewException notFoundReviewException)
            {
                throw CreateAndLogValidationException(notFoundReviewException);
            }
            catch (SqlException sqlException)
            {
                var failedReviewStorageException = new FailedReviewStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedReviewStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsReviewException =
                    new AlreadyExistsReviewException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsReviewException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedReviewException = new LockedReviewException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedReviewException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedReviewStorageException =
                    new FailedReviewStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedReviewStorageException);
            }
            catch (Exception exception)
            {
                var failedReviewServiceException =
                    new FailedReviewServiceException(exception);

                throw CreateAndLogServiceException(failedReviewServiceException);
            }
        }

        private IQueryable<Review> TryCatch(ReturningReviewsFunction returningReviewsFunction)
        {
            try
            {
                return returningReviewsFunction();
            }
            catch (SqlException sqlException)
            {
                var failedReviewStorageException = new FailedReviewStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedReviewStorageException);
            }
            catch (Exception exception)
            {
                var failedReviewServiceException =
                    new FailedReviewServiceException(exception);

                throw CreateAndLogServiceException(failedReviewServiceException);
            }
        }

        private ReviewValidationException CreateAndLogValidationException(Xeption exception)
        {
            var reviewValidationException = new ReviewValidationException(exception);
            this.loggingBroker.LogError(reviewValidationException);

            return reviewValidationException;
        }

        private ReviewDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var reviewDependencyException = new ReviewDependencyException(exception);
            this.loggingBroker.LogCritical(reviewDependencyException);

            return reviewDependencyException;
        }

        private ReviewDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var reviewDependencyValidationException =
                new ReviewDependencyValidationException(exception);

            this.loggingBroker.LogError(reviewDependencyValidationException);

            return reviewDependencyValidationException;
        }

        private ReviewDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var reviewDependencyException = new ReviewDependencyException(exception);
            this.loggingBroker.LogError(reviewDependencyException);

            return reviewDependencyException;
        }

        private ReviewServiceException CreateAndLogServiceException(Xeption exception)
        {
            var reviewServiceException = new ReviewServiceException(exception);
            this.loggingBroker.LogError(reviewServiceException);

            return reviewServiceException;
        }
    }
}