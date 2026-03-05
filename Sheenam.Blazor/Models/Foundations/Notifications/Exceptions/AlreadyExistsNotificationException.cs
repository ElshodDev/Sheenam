//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Notifications.Exceptions
{
    public class AlreadyExistsNotificationException : Xeption
    {
        public AlreadyExistsNotificationException(Xeption innerException)
            : base(message: "Notification with the same id already exists.", innerException) { }
    }
}