//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertySales.Exceptions
{
    public class FailedPropertySaleStorageException : Xeption
    {
        public FailedPropertySaleStorageException(Exception innerException)
            : base(message: "Failed property sale storage error occured, contact support",
                  innerException)
        { }
    }
}
