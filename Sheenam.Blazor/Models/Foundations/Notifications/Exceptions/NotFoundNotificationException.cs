//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Notifications.Exceptions
{
    public class NotFoundNotificationException : Xeption
    {
        public NotFoundNotificationException(Guid notificationId)
            : base(message: $"Notification not found with id: {notificationId}.") { }
    }
}