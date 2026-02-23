//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Reviews.Exceptions
{
    public class AlreadyExistsReviewException : Xeption
    {
        public AlreadyExistsReviewException(Exception innerException)
            : base(message: "Review already exists.", innerException)
        { }
    }
}
