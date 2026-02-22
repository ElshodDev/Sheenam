//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Reviews.Exceptions
{
    public class NotFoundReviewException : Xeption
    {
        public NotFoundReviewException(Exception innerException)
            : base(message: "Review not found.", innerException)
        { }
    }
}
