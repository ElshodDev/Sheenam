//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertySales.Exceptions
{
    public class PropertySaleValidationException : Xeption
    {
        public PropertySaleValidationException(Xeption innerException)
            : base(message: "PropertySale validation error occured, fix the errors and try again",
                  innerException)
        { }

        public PropertySaleValidationException(string message)
            : base(message)
        { }
    }
}