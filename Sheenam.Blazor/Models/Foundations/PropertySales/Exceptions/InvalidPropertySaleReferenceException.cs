//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.PropertySales.Exceptions
{
    public class InvalidPropertySaleReferenceException : Xeption
    {
        public InvalidPropertySaleReferenceException(Exception innerException)
            : base(message: "Invalid property sale reference error occurred.", innerException) { }
    }
}