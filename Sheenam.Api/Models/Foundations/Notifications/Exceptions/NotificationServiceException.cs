//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Notifications.Exceptions
{
    public class NotificationServiceException : Xeption
    {
        public NotificationServiceException(Xeption innerException)
            : base(message: "Notification service error occurred, contact support.", innerException)
        { }
    }
}
