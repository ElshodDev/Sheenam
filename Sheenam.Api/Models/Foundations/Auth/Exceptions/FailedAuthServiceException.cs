//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Auth.Exceptions
{
    public class FailedAuthServiceException : Xeption
    {
        public FailedAuthServiceException(Exception innerException)
            : base(message: "Failed auth service error occurred, contact support.",
                  innerException)
        { }
    }
}