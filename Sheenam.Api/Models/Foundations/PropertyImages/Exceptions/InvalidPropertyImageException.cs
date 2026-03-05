//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyImages.Exceptions
{
    public class InvalidPropertyImageException : Xeption
    {
        public InvalidPropertyImageException()
            : base(message: "PropertyImage is invalid.") { }
    }
}
