//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Properties.Exceptions
{
    public class NullPropertyException : Xeption
    {
        public NullPropertyException()
            : base(message: "Property is null.") { }
    }
}