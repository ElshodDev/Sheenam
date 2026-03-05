//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyImages.Exceptions
{
    public class FailedPropertyImageServiceException : Xeption
    {
        public FailedPropertyImageServiceException(Exception innerException)
            : base(message: "Failed property image service error occurred.", innerException) { }
    }
}
