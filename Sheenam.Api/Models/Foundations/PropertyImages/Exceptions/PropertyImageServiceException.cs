//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyImages.Exceptions
{
    public class PropertyImageServiceException : Xeption
    {
        public PropertyImageServiceException(Xeption innerException)
            : base(message: "PropertyImage service error occurred.", innerException) { }
    }
}
