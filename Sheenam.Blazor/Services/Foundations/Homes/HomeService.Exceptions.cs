//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Homes;
using Sheenam.Blazor.Models.Foundations.Homes.Exceptions;
using Xeptions;

namespace Sheenam.Blazor.Services.Foundations.Homes
{
    public partial class HomeService
    {
        private delegate ValueTask<Home> ReturningHomeFunction();
        private delegate ValueTask<List<Home>> ReturningHomesFunction();

        private async ValueTask<Home> TryCatch(ReturningHomeFunction returningHomeFunction)
        {
            try
            {
                return await returningHomeFunction();
            }
            catch (NullHomeException nullHomeException)
            {
                throw CreateAndLogValidationException(nullHomeException);
            }
            catch (InvalidHomeException invalidHomeException)
            {
                throw CreateAndLogValidationException(invalidHomeException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidHomeReferenceException =
                    new InvalidHomeReferenceException(httpResponseBadRequestException);

                throw CreateAndLogDependencyValidationException(invalidHomeReferenceException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsHomeException =
                    new AlreadyExistsHomeException(httpResponseConflictException);

                throw CreateAndLogDependencyValidationException(alreadyExistsHomeException);
            }
            catch (Exception exception)
            {
                var failedHomeServiceException =
                    new FailedHomeServiceException(exception);

                var homeServiceException =
                    new HomeServiceException(failedHomeServiceException);

                this.loggingBroker.LogError(homeServiceException);
                throw homeServiceException;
            }
        }

        private async ValueTask<List<Home>> TryCatch(ReturningHomesFunction returningHomesFunction)
        {
            try
            {
                return await returningHomesFunction();
            }
            catch (Exception exception)
            {
                var failedHomeServiceException =
                    new FailedHomeServiceException(exception);

                var homeServiceException =
                    new HomeServiceException(failedHomeServiceException);

                this.loggingBroker.LogError(homeServiceException);
                throw homeServiceException;
            }
        }

        private HomeValidationException CreateAndLogValidationException(Xeption exception)
        {
            var homeValidationException = new HomeValidationException(exception);
            this.loggingBroker.LogError(homeValidationException);

            return homeValidationException;
        }

        private HomeDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var homeDependencyValidationException = new HomeDependencyValidationException(exception);
            this.loggingBroker.LogError(homeDependencyValidationException);

            return homeDependencyValidationException;
        }
    }
}