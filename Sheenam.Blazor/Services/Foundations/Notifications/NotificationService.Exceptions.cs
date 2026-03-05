//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Notifications;
using Sheenam.Blazor.Models.Foundations.Notifications.Exceptions;
using Xeptions;
namespace Sheenam.Blazor.Services.Foundations.Notifications
{
    public partial class NotificationService
    {
        private delegate ValueTask<Notification> ReturningNotificationFunction();
        private delegate ValueTask<IQueryable<Notification>> ReturningNotificationsFunction();

        private async ValueTask<Notification> TryCatch(ReturningNotificationFunction function)
        {
            try
            {
                return await function();
            }
            catch (NullNotificationException nullNotificationException)
            {
                throw CreateAndLogValidationException(nullNotificationException);
            }
            catch (InvalidNotificationException invalidNotificationException)
            {
                throw CreateAndLogValidationException(invalidNotificationException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidNotificationReferenceException =
                    new InvalidNotificationReferenceException(httpResponseBadRequestException);
                throw CreateAndLogDependencyValidationException(invalidNotificationReferenceException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var alreadyExistsNotificationException =
                    new AlreadyExistsNotificationException(httpResponseConflictException);
                throw CreateAndLogDependencyValidationException(alreadyExistsNotificationException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedNotificationDependencyException =
                    new FailedNotificationDependencyException(httpResponseException);
                throw CreateAndLogDependencyException(failedNotificationDependencyException);
            }
            catch (Exception exception)
            {
                var failedNotificationServiceException =
                    new FailedNotificationServiceException(exception);
                throw CreateAndLogServiceException(failedNotificationServiceException);
            }
        }

        private async ValueTask<IQueryable<Notification>> TryCatch(ReturningNotificationsFunction function)
        {
            try
            {
                return await function();
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedNotificationDependencyException =
                    new FailedNotificationDependencyException(httpResponseException);
                throw CreateAndLogDependencyException(failedNotificationDependencyException);
            }
            catch (Exception exception)
            {
                var failedNotificationServiceException =
                    new FailedNotificationServiceException(exception);
                throw CreateAndLogServiceException(failedNotificationServiceException);
            }
        }

        private NotificationValidationException CreateAndLogValidationException(Xeption exception)
        {
            var notificationValidationException = new NotificationValidationException(exception);
            this.loggingBroker.LogError(notificationValidationException);
            return notificationValidationException;
        }

        private NotificationDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var notificationDependencyValidationException =
                new NotificationDependencyValidationException(exception);
            this.loggingBroker.LogError(notificationDependencyValidationException);
            return notificationDependencyValidationException;
        }

        private NotificationDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var notificationDependencyException = new NotificationDependencyException(exception);
            this.loggingBroker.LogError(notificationDependencyException);
            return notificationDependencyException;
        }

        private NotificationServiceException CreateAndLogServiceException(Xeption exception)
        {
            var notificationServiceException = new NotificationServiceException(exception);
            this.loggingBroker.LogError(notificationServiceException);
            return notificationServiceException;
        }
    }
}