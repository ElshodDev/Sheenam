//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Reviews.Exceptions
{
    public class FailedReviewDependencyException : Xeption
    {
        public FailedReviewDependencyException(Exception innerException)
            : base(message: "Failed review dependency error occurred, contact support.", innerException)
        { }
    }
}
