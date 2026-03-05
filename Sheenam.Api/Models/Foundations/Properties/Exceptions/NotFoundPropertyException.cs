//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Properties.Exceptions
{
    public class NotFoundPropertyException : Xeption
    {
        public NotFoundPropertyException(Guid propertyId)
            : base(message: $"Could not find property with id: {propertyId}.") { }
    }
}