//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Notifications.Exceptions
{
    public class NullNotificationException : Xeption
    {
        public NullNotificationException()
            : base(message: "Notification is null.") { }
    }
}