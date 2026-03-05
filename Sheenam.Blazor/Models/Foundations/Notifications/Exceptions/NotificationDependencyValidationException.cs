//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Notifications.Exceptions
{
    public class NotificationDependencyValidationException : Xeption
    {
        public NotificationDependencyValidationException(Xeption innerException)
            : base(message: "Notification dependency validation error occurred.", innerException) { }
    }
}