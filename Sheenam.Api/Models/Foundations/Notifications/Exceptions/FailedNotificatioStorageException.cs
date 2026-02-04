//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Notifications.Exceptions
{
    public class FailedNotificatioStorageException : Xeption
    {
        public FailedNotificatioStorageException(Exception innerException)
            : base(message: "Failed notification storage error occurred, contact support.",
                  innerException)
        { }
    }
}

