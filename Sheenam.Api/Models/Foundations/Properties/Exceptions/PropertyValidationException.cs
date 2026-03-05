//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Properties.Exceptions
{
    public class PropertyValidationException : Xeption
    {
        public PropertyValidationException(Xeption innerException)
            : base(message: "Property validation error occurred.", innerException) { }
    }
}