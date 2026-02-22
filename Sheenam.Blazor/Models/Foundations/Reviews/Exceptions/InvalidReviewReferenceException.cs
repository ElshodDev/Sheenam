//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Reviews.Exceptions
{
    public class InvalidReviewReferenceException : Xeption
    {
        public InvalidReviewReferenceException(Exception innerException)
            : base(message: "Invalid review reference error occurred.", innerException)
        { }
    }
}
