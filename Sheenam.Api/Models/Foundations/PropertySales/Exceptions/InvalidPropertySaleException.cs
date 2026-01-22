//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertySales.Exceptions
{
    public class InvalidPropertySaleException : Xeption
    {
        public InvalidPropertySaleException()
            : base(message: "PropertySale is invalid")
        { }
    }
}
