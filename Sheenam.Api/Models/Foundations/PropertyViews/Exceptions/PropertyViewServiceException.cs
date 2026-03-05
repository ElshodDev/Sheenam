//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyViews.Exceptions
{
    public class PropertyViewServiceException : Xeption
    {
        public PropertyViewServiceException(Xeption innerException)
            : base(message: "PropertyView service error occurred.", innerException) { }
    }
}
