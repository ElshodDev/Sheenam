//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Notifications.Exceptions
{
    public class NotificationDependencyException : Xeption
    {
        public NotificationDependencyException(Xeption innerException)
            : base(message: "Notification dependency error occurred, contact support.", innerException)
        { }
    }
}
