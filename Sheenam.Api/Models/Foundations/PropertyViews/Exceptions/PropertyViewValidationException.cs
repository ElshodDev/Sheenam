//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyViews.Exceptions
{
    public class PropertyViewValidationException : Xeption
    {
        public PropertyViewValidationException(Xeption innerException)
            : base(message: "PropertyView validation error occurred.", innerException) { }
    }
}
