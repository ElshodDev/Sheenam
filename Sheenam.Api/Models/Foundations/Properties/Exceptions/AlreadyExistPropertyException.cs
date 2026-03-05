//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Properties.Exceptions
{
    public class AlreadyExistPropertyException : Xeption
    {
        public AlreadyExistPropertyException(Exception innerException)
            : base(message: "Property already exists.", innerException) { }
    }
}