//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Reviews.Exceptions
{
    public class NullReviewException : Xeption
    {
        public NullReviewException()
            : base(message: "Review is null.")
        { }
    }
}
