//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.PropertySales.Exceptions
{
    public class NotFoundPropertySaleException : Xeption
    {
        public NotFoundPropertySaleException(Guid propertySaleId)
            : base(message: $"Property sale not found with id: {propertySaleId}.") { }
    }
}