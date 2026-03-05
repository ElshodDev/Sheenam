//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyViews.Exceptions
{
    public class NotFoundPropertyViewException : Xeption
    {
        public NotFoundPropertyViewException(Guid propertyViewId)
            : base(message: $"Could not find property view with id: {propertyViewId}.") { }
    }
}
