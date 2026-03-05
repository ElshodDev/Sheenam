//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyViews.Exceptions
{
    public class AlreadyExistsPropertyViewException : Xeption
    {
        public AlreadyExistsPropertyViewException(Exception innerException)
            : base(message: "PropertyView already exists.", innerException) { }
    }
}
