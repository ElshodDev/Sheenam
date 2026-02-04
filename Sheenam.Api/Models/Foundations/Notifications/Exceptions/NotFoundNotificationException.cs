//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Notifications.Exceptions
{
    public class NotFoundNotificationException : Xeption
    {
        public NotFoundNotificationException(Guid notificationId)
            : base(message: $"Couldn't find notification with id: {notificationId}.")
        { }
    }
}
