//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.PropertySales.Exceptions
{
    public class FailedPropertySaleServiceException : Xeption
    {
        public FailedPropertySaleServiceException(Exception innerException)
            : base(message: "Failed property sale service error occurred.", innerException) { }
    }
}