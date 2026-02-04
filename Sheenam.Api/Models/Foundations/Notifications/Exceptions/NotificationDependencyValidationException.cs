//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Notifications.Exceptions
{
    public class NotificationDependencyValidationException : Xeption
    {
        public NotificationDependencyValidationException(Xeption innerException)
            : base(message: "Notification dependency validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
