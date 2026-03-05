//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Properties.Exceptions
{
    public class PropertyServiceException : Xeption
    {
        public PropertyServiceException(Xeption innerException)
            : base(message: "Property service error occurred.", innerException) { }
    }
}