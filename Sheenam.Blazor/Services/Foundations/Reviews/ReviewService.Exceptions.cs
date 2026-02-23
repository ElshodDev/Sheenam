//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Reviews;
using Sheenam.Blazor.Models.Foundations.Reviews.Exceptions;
using Xeptions;

namespace Sheenam.Blazor.Services.Foundations.Reviews
{
    public partial class ReviewService
    {
        private delegate ValueTask<Review> ReturningReviewFunction();
        private delegate ValueTask<IQueryable<Review>> ReturningReviewsFunction();

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
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidReviewReferenceException =
                    new InvalidReviewReferenceException(httpResponseBadRequestException);

                throw CreateAndLogDependencyValidationException(invalidReviewReferenceException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsReviewException =
                    new AlreadyExistsReviewException(httpResponseConflictException);

                throw CreateAndLogDependencyValidationException(alreadyExistsReviewException);
            }
            catch (HttpResponseLockedException httpResponseLockedException)
            {
                var lockedReviewException =
                    new LockedReviewException(httpResponseLockedException);

                throw CreateAndLogDependencyValidationException(lockedReviewException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundReviewException =
                    new NotFoundReviewException(httpResponseNotFoundException);

                throw CreateAndLogDependencyValidationException(notFoundReviewException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedReviewDependencyException =
                    new FailedReviewDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedReviewDependencyException);
            }
            catch (Exception exception)
            {
                var failedReviewServiceException =
                    new FailedReviewServiceException(exception);

                throw CreateAndLogServiceException(failedReviewServiceException);
            }
        }

        private async ValueTask<IQueryable<Review>> TryCatch(ReturningReviewsFunction returningReviewsFunction)
        {
            try
            {
                return await returningReviewsFunction();
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedReviewDependencyException =
                    new FailedReviewDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogDependencyException(failedReviewDependencyException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var failedReviewDependencyException =
                    new FailedReviewDependencyException(httpResponseForbiddenException);

                throw CreateAndLogDependencyException(failedReviewDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedReviewDependencyException =
                    new FailedReviewDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedReviewDependencyException);
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

        private ReviewDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var reviewDependencyValidationException = new ReviewDependencyValidationException(exception);
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
