//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertySales.Exceptions
{
    public class NotFoundPropertySaleException : Xeption
    {
        public NotFoundPropertySaleException(Guid id)
            : base(message: $"Couldn't find property sale with id: {id}.")
        { }

        public NotFoundPropertySaleException(string message)
            : base(message)
        { }
    }
}