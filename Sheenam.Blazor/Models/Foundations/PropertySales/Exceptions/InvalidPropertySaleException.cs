//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.PropertySales.Exceptions
{
    public class InvalidPropertySaleException : Xeption
    {
        public InvalidPropertySaleException()
            : base(message: "Invalid property sale, fix errors and try again.") { }
    }
}