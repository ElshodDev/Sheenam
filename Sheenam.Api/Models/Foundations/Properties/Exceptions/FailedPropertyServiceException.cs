//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Properties.Exceptions
{
    public class FailedPropertyServiceException : Xeption
    {
        public FailedPropertyServiceException(Exception innerException)
            : base(message: "Failed property service error occurred.", innerException) { }
    }
}