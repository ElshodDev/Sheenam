//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertySales.Exceptions
{
    public class LockedPropertySaleException : Xeption
    {
        public LockedPropertySaleException(Exception innerException)
            : base(message: "The property sale is locked. Please try again later.", innerException)
        { }
    }
}
