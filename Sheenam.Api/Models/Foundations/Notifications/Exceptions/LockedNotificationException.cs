//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Notifications.Exceptions
{
    public class LockedNotificationException : Xeption
    {
        public LockedNotificationException(Exception innerException)
            : base(message: "Locked notification error occurred. Please try again later.", innerException)
        { }
    }
}
