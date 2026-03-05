//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyImages.Exceptions
{
    public class NotFoundPropertyImageException : Xeption
    {
        public NotFoundPropertyImageException(Guid propertyImageId)
            : base(message: $"Could not find property image with id: {propertyImageId}.") { }
    }
}
