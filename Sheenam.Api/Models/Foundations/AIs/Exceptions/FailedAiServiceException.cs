//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.AIs.Exceptions
{
    public class FailedAiServiceException : Xeption
    {
        public FailedAiServiceException(Exception innerException)
            : base(message: "Failed AI service error occurred, contact support.", innerException)
        { }
    }
}
