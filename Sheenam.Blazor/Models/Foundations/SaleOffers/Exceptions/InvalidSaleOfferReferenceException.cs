//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleOffers.Exceptions
{
    public class InvalidSaleOfferReferenceException : Xeption
    {
        public InvalidSaleOfferReferenceException(Exception innerException)
            : base(message: "Invalid sale offer reference error occurred.", innerException) { }
    }
}