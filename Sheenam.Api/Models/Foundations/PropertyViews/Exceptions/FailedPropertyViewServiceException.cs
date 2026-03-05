//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyViews.Exceptions
{
    public class FailedPropertyViewServiceException : Xeption
    {
        public FailedPropertyViewServiceException(Exception innerException)
            : base(message: "Failed property view service error occurred.", innerException) { }
    }
}
