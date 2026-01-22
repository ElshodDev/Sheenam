//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertySales.Exceptions
{
    public class AlreadyExistPropertySaleException : Xeption
    {
        public AlreadyExistPropertySaleException(Exception innerException)
            : base(message: "PropertySale already exists", innerException)
        { }
    }
}