//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyImages.Exceptions
{
    public class PropertyImageDependencyException : Xeption
    {
        public PropertyImageDependencyException(Xeption innerException)
            : base(message: "PropertyImage dependency error occurred.", innerException) { }
    }
}
