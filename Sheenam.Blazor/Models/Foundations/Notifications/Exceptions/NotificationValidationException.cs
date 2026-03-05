//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Notifications.Exceptions
{
    public class NotificationValidationException : Xeption
    {
        public NotificationValidationException(Xeption innerException)
            : base(message: "Notification validation error occurred.", innerException) { }
    }
}