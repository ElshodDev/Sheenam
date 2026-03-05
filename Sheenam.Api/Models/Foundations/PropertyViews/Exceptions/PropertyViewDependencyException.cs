//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyViews.Exceptions
{
    public class PropertyViewDependencyException : Xeption
    {
        public PropertyViewDependencyException(Xeption innerException)
            : base(message: "PropertyView dependency error occurred.", innerException) { }
    }
}
