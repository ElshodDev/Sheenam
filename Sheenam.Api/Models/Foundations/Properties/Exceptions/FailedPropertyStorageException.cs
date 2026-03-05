//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Properties.Exceptions
{
    public class FailedPropertyStorageException : Xeption
    {
        public FailedPropertyStorageException(Exception innerException)
            : base(message: "Failed property storage error occurred.", innerException) { }
    }
}