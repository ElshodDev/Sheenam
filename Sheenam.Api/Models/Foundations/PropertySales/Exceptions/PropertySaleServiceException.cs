//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertySales.Exceptions
{
    public class PropertySaleServiceException : Xeption
    {
        public PropertySaleServiceException(Xeption serviceException)
            : base(message: "PropertySale service error occured, contact support",
                  serviceException)
        { }
    }
}
