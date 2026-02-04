//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Notifications.Exceptions
{
    public class FailedNotificationServiceException : Xeption
    {
        public FailedNotificationServiceException(Exception innerException)
            : base(message: "Failed notification service error occurred, contact support.", innerException)
        { }
    }
}
