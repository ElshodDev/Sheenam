//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Reviews.Exceptions
{
    public class NullReviewException : Xeption
    {
        public NullReviewException()
            : base(message: "The review is null.")
        { }
    }
}
