//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Properties.Exceptions
{
    public class InvalidPropertyException : Xeption
    {
        public InvalidPropertyException()
            : base(message: "Property is invalid.") { }
    }
}