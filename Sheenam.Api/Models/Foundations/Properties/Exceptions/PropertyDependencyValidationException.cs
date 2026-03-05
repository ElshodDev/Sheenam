//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Properties.Exceptions
{
    public class PropertyDependencyValidationException : Xeption
    {
        public PropertyDependencyValidationException(Xeption innerException)
            : base(message: "Property dependency validation error occurred.", innerException) { }
    }
}