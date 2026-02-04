//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Notifications;
using Sheenam.Api.Models.Foundations.Notifications.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.Notifications
{
    public partial class NotificationService
    {
        private void ValidateNotificationOnAdd(Notification notification)
        {
            ValidateNotificationNotNull(notification);
            Validate(
              // Id will be generated in service, no need to validate on Add
              (Rule: IsInvalid(notification.UserId), Parameter: nameof(Notification.UserId)),
              (Rule: IsInvalid(notification.Message), Parameter: nameof(Notification.Message)));
        }

        private void ValidateNotificationOnModify(Notification notification)
        {
            ValidateNotificationNotNull(notification);
            Validate(
              (Rule: IsInvalid(notification.Id), Parameter: nameof(Notification.Id)),
              (Rule: IsInvalid(notification.UserId), Parameter: nameof(Notification.UserId)),
              (Rule: IsInvalid(notification.Message), Parameter: nameof(Notification.Message)));
        }

        private static void ValidateNotificationId(Guid notificationId) =>
            Validate((Rule: IsInvalid(notificationId), Parameter: nameof(Notification.Id)));

        private static void ValidateUserId(Guid userId) =>
            Validate((Rule: IsInvalid(userId), Parameter: "UserId"));

        private static void ValidateStorageNotification(Notification maybeNotification, Guid id)
        {
            if (maybeNotification is null)
            {
                throw new NotFoundNotificationException(id);
            }
        }

        private void ValidateNotificationNotNull(Notification notification)
        {
            if (notification is null)
            {
                throw new NullNotificationException();
            }
        }

        private static dynamic IsInvalid(Guid Id) => new
        {
            Condition = Id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidNotificationException = new InvalidNotificationException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidNotificationException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidNotificationException.ThrowIfContainsErrors();
        }
    }
}
