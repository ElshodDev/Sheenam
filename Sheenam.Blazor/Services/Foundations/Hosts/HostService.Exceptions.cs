//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Hosts.Exceptions;
using Xeptions;
using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Services.Foundations.Hosts
{
    public partial class HostService
    {
        private delegate ValueTask<HostModel> ReturningHostFunction();
        private delegate ValueTask<List<HostModel>> ReturningHostsFunction();

        private async ValueTask<HostModel> TryCatch(ReturningHostFunction returningHostFunction)
        {
            try
            {
                return await returningHostFunction();
            }
            catch (NullHostException nullHostException)
            {
                throw CreateAndLogValidationException(nullHostException);
            }
            catch (InvalidHostException invalidHostException)
            {
                throw CreateAndLogValidationException(invalidHostException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidHostReferenceException =
                    new InvalidHostReferenceException(httpResponseBadRequestException);

                throw CreateAndLogDependencyValidationException(invalidHostReferenceException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsHostException =
                    new AlreadyExistsHostException(httpResponseConflictException);

                throw CreateAndLogDependencyValidationException(alreadyExistsHostException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedHostDependencyException =
                    new FailedHostDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedHostDependencyException);
            }
            catch (Exception exception)
            {
                var failedHostServiceException =
                    new FailedHostServiceException(exception);

                throw CreateAndLogServiceException(failedHostServiceException);
            }
        }

        private async ValueTask<List<HostModel>> TryCatch(ReturningHostsFunction returningHostsFunction)
        {
            try
            {
                return await returningHostsFunction();
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedHostDependencyException =
                    new FailedHostDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedHostDependencyException);
            }
            catch (Exception exception)
            {
                var failedHostServiceException =
                    new FailedHostServiceException(exception);

                throw CreateAndLogServiceException(failedHostServiceException);
            }
        }

        private HostValidationException CreateAndLogValidationException(Xeption exception)
        {
            var hostValidationException = new HostValidationException(exception);
            this.loggingBroker.LogError(hostValidationException);
            return hostValidationException;
        }

        private HostDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var hostDependencyValidationException = new HostDependencyValidationException(exception);
            this.loggingBroker.LogError(hostDependencyValidationException);
            return hostDependencyValidationException;
        }

        private HostDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var hostDependencyException = new HostDependencyException(exception);
            this.loggingBroker.LogError(hostDependencyException);
            return hostDependencyException;
        }

        private HostServiceException CreateAndLogServiceException(Xeption exception)
        {
            var hostServiceException = new HostServiceException(exception);
            this.loggingBroker.LogError(hostServiceException);
            return hostServiceException;
        }
    }
}
