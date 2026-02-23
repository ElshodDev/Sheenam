//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Reviews.Exceptions
{
    public class ReviewDependencyValidationException : Xeption
    {
        public ReviewDependencyValidationException(Xeption innerException)
            : base(message: "Review dependency validation error occurred, fix errors and try again.",
                  innerException)
        { }
    }
}
