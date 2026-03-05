//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyImages.Exceptions
{
    public class FailedPropertyImageStorageException : Xeption
    {
        public FailedPropertyImageStorageException(Exception innerException)
            : base(message: "Failed property image storage error occurred.", innerException) { }
    }
}
