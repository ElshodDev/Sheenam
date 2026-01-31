//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Reviews.Exceptions
{
    public class ReviewValidationException : Xeption
    {
        public ReviewValidationException(Xeption innerException)
            : base(message: "Review validation error occurred, fix the errors and try again.", innerException)
        { }
    }
}
