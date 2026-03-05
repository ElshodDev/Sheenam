//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Notifications.Exceptions
{
    public class InvalidNotificationReferenceException : Xeption
    {
        public InvalidNotificationReferenceException(Exception innerException)
            : base(message: "Invalid notification reference error occurred.", innerException) { }
    }
}