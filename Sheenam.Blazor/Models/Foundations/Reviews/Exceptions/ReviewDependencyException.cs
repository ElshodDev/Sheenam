//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Reviews.Exceptions
{
    public class ReviewDependencyException : Xeption
    {
        public ReviewDependencyException(Xeption innerException)
            : base(message: "Review dependency error occurred, contact support.", innerException)
        { }
    }
}
