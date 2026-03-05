//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Messages.Exceptions
{
    public class FailedMessageServiceException : Xeption
    {
        public FailedMessageServiceException(Exception innerException)
            : base(message: "Failed message service error occurred.", innerException) { }
    }
}
