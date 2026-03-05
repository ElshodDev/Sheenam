//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Properties.Exceptions
{
    public class PropertyDependencyException : Xeption
    {
        public PropertyDependencyException(Xeption innerException)
            : base(message: "Property dependency error occurred.", innerException) { }
    }
}