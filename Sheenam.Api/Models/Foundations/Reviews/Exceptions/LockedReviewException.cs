//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Reviews.Exceptions
{
    public class LockedReviewException : Xeption
    {
        public LockedReviewException(Exception innerException)
            : base(message: "Review is locked. Please try again later.", innerException)
        { }
    }
}
