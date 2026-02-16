//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Notifications;
using Sheenam.Api.Models.Foundations.Notifications.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.Notifications
{
    public partial class NotificationService
    {
        private delegate ValueTask<Notification> ReturningNotificationFunction();
        private delegate IQueryable<Notification> ReturningNotificationsFunction();

        private async ValueTask<Notification> TryCatch(
            ReturningNotificationFunction returningNotificationFunction)
        {
            try
            {
                return await returningNotificationFunction();
            }
            catch (NullNotificationException nullNotificationException)
            {
                throw CreateAndLogValidationException(nullNotificationException);
            }
            catch (InvalidNotificationException invalidNotificationException)
            {
                throw CreateAndLogValidationException(invalidNotificationException);
            }
            catch (NotFoundNotificationException notFoundNotificationException)
            {
                throw CreateAndLogValidationException(notFoundNotificationException);
            }
            catch (SqlException sqlException)
            {
                var failedNotificationStorageException = new FailedNotificatioStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedNotificationStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsNotificationException = new AlreadyExistsNotificationException(duplicateKeyException);
                throw CreateAndLogDependencyValidationException(alreadyExistsNotificationException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedNotificationException = new LockedNotificationException(dbUpdateConcurrencyException);
                throw CreateAndLogDependencyException(lockedNotificationException);
            }
            catch (Exception exception)
            {
                var failedNotificationServiceException = new FailedNotificationServiceException(exception);
                throw CreateAndLogServiceException(failedNotificationServiceException);
            }
        }

        private IQueryable<Notification> TryCatch(ReturningNotificationsFunction returningNotificationsFunction)
        {
            try
            {
                return returningNotificationsFunction();
            }
            catch (SqlException sqlException)
            {
                var failedNotificationStorageException = new FailedNotificatioStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedNotificationStorageException);
            }
            catch (Exception exception)
            {
                var failedNotificationServiceException = new FailedNotificationServiceException(exception);
                throw CreateAndLogServiceException(failedNotificationServiceException);
            }
        }

        private NotificationValidationException CreateAndLogValidationException(Xeption exception)
        {
            var notificationValidationException = new NotificationValidationException(exception);
            this.loggingBroker.LogError(notificationValidationException);

            return notificationValidationException;
        }

        private NotificationDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var notificationDependencyException = new NotificationDependencyException(exception);
            this.loggingBroker.LogCritical(notificationDependencyException);

            return notificationDependencyException;
        }

        private NotificationDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var notificationDependencyValidationException = new NotificationDependencyValidationException(exception);
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