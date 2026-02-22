//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Reviews.Exceptions
{
    public class ReviewValidationException : Xeption
    {
        public ReviewValidationException(Xeption innerException)
            : base(message: "Review validation error occurred, fix errors and try again.", innerException)
        { }
    }
}
