//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Homes.Exceptions
{
    public class FailedHomeDependencyException : Xeption
    {
        public FailedHomeDependencyException(Exception innerException)
            : base(message: "Failed home dependency error occurred, contact support.", innerException)
        { }
    }
}
