//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Notifications.Exceptions
{
    public class NotificationValidationException : Xeption
    {
        public NotificationValidationException(Xeption innerException)
            : base(message: "Notification validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
