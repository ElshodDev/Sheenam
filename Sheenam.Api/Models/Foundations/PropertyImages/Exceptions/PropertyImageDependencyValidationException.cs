//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyImages.Exceptions
{
    public class PropertyImageDependencyValidationException : Xeption
    {
        public PropertyImageDependencyValidationException(Xeption innerException)
            : base(message: "PropertyImage dependency validation error occurred.", innerException) { }
    }
}
