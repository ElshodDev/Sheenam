//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Notifications.Exceptions
{
    public class FailedNotificationDependencyException : Xeption
    {
        public FailedNotificationDependencyException(Exception innerException)
            : base(message: "Failed notification dependency error occurred.", innerException) { }
    }
}