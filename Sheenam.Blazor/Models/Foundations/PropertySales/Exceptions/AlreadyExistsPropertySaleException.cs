//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.PropertySales.Exceptions
{
    public class AlreadyExistsPropertySaleException : Xeption
    {
        public AlreadyExistsPropertySaleException(Xeption innerException)
            : base(message: "Property sale with the same id already exists.", innerException) { }
    }
}