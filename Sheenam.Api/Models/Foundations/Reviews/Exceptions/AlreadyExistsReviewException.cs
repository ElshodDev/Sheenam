//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Reviews.Exceptions
{
    public class AlreadyExistsReviewException : Xeption
    {
        public AlreadyExistsReviewException(Exception innerException)
            : base(message: "Review with the same key already exists.", innerException)
        { }
    }
}
