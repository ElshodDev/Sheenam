//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Homes;
using Sheenam.Blazor.Models.Foundations.Homes.Exceptions;
using Xeptions;
using HomeDependencyExceptionModel = Sheenam.Blazor.Models.Foundations.Homes.Exceptions.HomeDependencyException;

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
            catch (HttpResponseException httpResponseException)
            {
                var failedHomeDependencyException =
                    new FailedHomeDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedHomeDependencyException);
            }
            catch (Exception exception)
            {
                var failedHomeServiceException =
                    new FailedHomeServiceException(exception);

                throw CreateAndLogServiceException(failedHomeServiceException);
            }
        }

        private async ValueTask<List<Home>> TryCatch(ReturningHomesFunction returningHomesFunction)
        {
            try
            {
                return await returningHomesFunction();
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedHomeDependencyException =
                    new FailedHomeDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedHomeDependencyException);
            }
            catch (Exception exception)
            {
                var failedHomeServiceException =
                    new FailedHomeServiceException(exception);

                throw CreateAndLogServiceException(failedHomeServiceException);
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

        private HomeDependencyExceptionModel CreateAndLogDependencyException(Xeption exception)
        {
            var homeDependencyException = new HomeDependencyExceptionModel(exception);
            this.loggingBroker.LogError(homeDependencyException);

            return homeDependencyException;
        }

        private HomeServiceException CreateAndLogServiceException(Xeption exception)
        {
            var homeServiceException = new HomeServiceException(exception);
            this.loggingBroker.LogError(homeServiceException);

            return homeServiceException;
        }
    }
}