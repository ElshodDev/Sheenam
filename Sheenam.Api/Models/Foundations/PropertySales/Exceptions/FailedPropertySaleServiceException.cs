//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertySales.Exceptions
{
    public class FailedPropertySaleServiceException : Xeption
    {
        public FailedPropertySaleServiceException(Exception serviceException)
            : base(message: "PropertySale service error occured, contact support",
                  serviceException)
        { }
    }
}