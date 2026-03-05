//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.Notifications;
using Sheenam.Blazor.Models.Foundations.Notifications.Exceptions;
namespace Sheenam.Blazor.Services.Foundations.Notifications
{
    public partial class NotificationService
    {
        private static void ValidateNotificationOnAdd(Notification notification)
        {
            ValidateNotificationNotNull(notification);
            Validate(
                (Rule: IsInvalid(notification.Id), Parameter: nameof(Notification.Id)),
                (Rule: IsInvalid(notification.UserId), Parameter: nameof(Notification.UserId)),
                (Rule: IsInvalid(notification.Title), Parameter: nameof(Notification.Title)),
                (Rule: IsInvalid(notification.Message), Parameter: nameof(Notification.Message)),
                (Rule: IsInvalid(notification.CreatedDate), Parameter: nameof(Notification.CreatedDate)));
        }

        private static void ValidateNotificationOnModify(Notification notification)
        {
            ValidateNotificationNotNull(notification);
            Validate(
                (Rule: IsInvalid(notification.Id), Parameter: nameof(Notification.Id)),
                (Rule: IsInvalid(notification.UserId), Parameter: nameof(Notification.UserId)),
                (Rule: IsInvalid(notification.Title), Parameter: nameof(Notification.Title)),
                (Rule: IsInvalid(notification.Message), Parameter: nameof(Notification.Message)),
                (Rule: IsInvalid(notification.CreatedDate), Parameter: nameof(Notification.CreatedDate)));
        }

        private static void ValidateNotificationId(Guid notificationId) =>
            Validate((Rule: IsInvalid(notificationId), Parameter: nameof(Notification.Id)));

        private static void ValidateNotificationNotNull(Notification notification)
        {
            if (notification is null)
                throw new NullNotificationException();
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
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