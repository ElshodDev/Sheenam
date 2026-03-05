//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyImages.Exceptions
{
    public class PropertyImageValidationException : Xeption
    {
        public PropertyImageValidationException(Xeption innerException)
            : base(message: "PropertyImage validation error occurred.", innerException) { }
    }
}
