//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Notifications.Exceptions
{
    public class NullNotificationException : Xeption
    {
        public NullNotificationException()
            : base(message: "The notification is null.")
        { }
    }
}
