//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Notifications.Exceptions
{
    public class InvalidNotificationException : Xeption
    {
        public InvalidNotificationException()
            : base(message: "Invalid notification. Please correct the errors and try again.")
        { }
    }
}
