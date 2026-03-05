//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyImages.Exceptions
{
    public class NullPropertyImageException : Xeption
    {
        public NullPropertyImageException()
            : base(message: "PropertyImage is null.") { }
    }
}
