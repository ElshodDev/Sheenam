//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Reviews.Exceptions
{
    public class NotFoundReviewException : Xeption
    {
        public NotFoundReviewException(Guid reviewId)
            : base(message: $"Couldn't find review with id: {reviewId}.")
        { }
    }
}
