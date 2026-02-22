//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Reviews.Exceptions
{
    public class ReviewServiceException : Xeption
    {
        public ReviewServiceException(Exception innerException)
            : base(message: "Review service error occurred, contact support.", innerException)
        { }
    }
}
