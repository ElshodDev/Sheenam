//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.PropertySales.Exceptions
{
    public class NullPropertySaleException : Xeption
    {
        public NullPropertySaleException()
            : base(message: "Property sale is null.") { }
    }
}