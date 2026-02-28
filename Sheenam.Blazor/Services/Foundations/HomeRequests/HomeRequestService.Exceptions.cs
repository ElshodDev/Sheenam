//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.HomeRequests;
using Sheenam.Blazor.Models.Foundations.HomeRequests.Exceptions;
using Xeptions;

namespace Sheenam.Blazor.Services.Foundations.HomeRequests
{
    public partial class HomeRequestService
    {
        private delegate ValueTask<HomeRequest> ReturningHomeRequestFunction();
        private delegate ValueTask<IQueryable<HomeRequest>> ReturningHomeRequestsFunction();

        private async ValueTask<HomeRequest> TryCatch(ReturningHomeRequestFunction returningHomeRequestFunction)
        {
            try
            {
                return await returningHomeRequestFunction();
            }
            catch (NullHomeRequestException nullHomeRequestException)
            {
                throw CreateAndLogValidationException(nullHomeRequestException);
            }
            catch (InvalidHomeRequestException invalidHomeRequestException)
            {
                throw CreateAndLogValidationException(invalidHomeRequestException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidHomeRequestReferenceException =
                    new InvalidHomeRequestReferenceException(httpResponseBadRequestException);

                throw CreateAndLogDependencyValidationException(invalidHomeRequestReferenceException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsHomeRequestException =
                    new AlreadyExistsHomeRequestException(httpResponseConflictException);

                throw CreateAndLogDependencyValidationException(alreadyExistsHomeRequestException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedHomeRequestDependencyException =
                    new FailedHomeRequestDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedHomeRequestDependencyException);
            }
            catch (Exception exception)
            {
                var failedHomeRequestServiceException =
                    new FailedHomeRequestServiceException(exception);

                throw CreateAndLogServiceException(failedHomeRequestServiceException);
            }
        }

        private async ValueTask<IQueryable<HomeRequest>> TryCatch(ReturningHomeRequestsFunction returningHomeRequestsFunction)
        {
            try
            {
                return await returningHomeRequestsFunction();
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedHomeRequestDependencyException =
                    new FailedHomeRequestDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedHomeRequestDependencyException);
            }
            catch (Exception exception)
            {
                var failedHomeRequestServiceException =
                    new FailedHomeRequestServiceException(exception);

                throw CreateAndLogServiceException(failedHomeRequestServiceException);
            }
        }

        private HomeRequestValidationException CreateAndLogValidationException(Xeption exception)
        {
            var homeRequestValidationException = new HomeRequestValidationException(exception);
            this.loggingBroker.LogError(homeRequestValidationException);
            return homeRequestValidationException;
        }

        private HomeRequestDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var homeRequestDependencyValidationException = new HomeRequestDependencyValidationException(exception);
            this.loggingBroker.LogError(homeRequestDependencyValidationException);
            return homeRequestDependencyValidationException;
        }

        private HomeRequestDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var homeRequestDependencyException = new HomeRequestDependencyException(exception);
            this.loggingBroker.LogError(homeRequestDependencyException);
            return homeRequestDependencyException;
        }

        private HomeRequestServiceException CreateAndLogServiceException(Xeption exception)
        {
            var homeRequestServiceException = new HomeRequestServiceException(exception);
            this.loggingBroker.LogError(homeRequestServiceException);
            return homeRequestServiceException;
        }
    }
}