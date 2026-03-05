//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyImages.Exceptions
{
    public class AlreadyExistsPropertyImageException : Xeption
    {
        public AlreadyExistsPropertyImageException(Exception innerException)
            : base(message: "PropertyImage already exists.", innerException) { }
    }
}
