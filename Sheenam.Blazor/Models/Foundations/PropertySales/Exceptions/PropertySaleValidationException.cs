//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.PropertySales.Exceptions
{
    public class PropertySaleValidationException : Xeption
    {
        public PropertySaleValidationException(Xeption innerException)
            : base(message: "Property sale validation error occurred.", innerException) { }
    }
}