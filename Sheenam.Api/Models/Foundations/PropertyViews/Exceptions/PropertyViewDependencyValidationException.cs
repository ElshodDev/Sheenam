//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyViews.Exceptions
{
    public class PropertyViewDependencyValidationException : Xeption
    {
        public PropertyViewDependencyValidationException(Xeption innerException)
            : base(message: "PropertyView dependency validation error occurred.", innerException) { }
    }
}
