//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Notifications.Exceptions
{
    public class FailedNotificationServiceException : Xeption
    {
        public FailedNotificationServiceException(Exception innerException)
            : base(message: "Failed notification service error occurred.", innerException) { }
    }
}