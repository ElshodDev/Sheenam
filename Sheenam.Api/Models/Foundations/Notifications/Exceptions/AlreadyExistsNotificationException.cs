//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Notifications.Exceptions
{
    public class AlreadyExistsNotificationException : Xeption
    {
        public AlreadyExistsNotificationException(Exception innerException)
            : base(message: "Notification with the same key already exists.", innerException)
        { }
    }
}

