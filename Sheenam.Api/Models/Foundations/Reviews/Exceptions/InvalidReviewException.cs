//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Reviews.Exceptions
{
    public class InvalidReviewException : Xeption
    {
        public InvalidReviewException()
            : base(message: "Invalid review. Please correct the errors and try again.")
        { }
    }
}
