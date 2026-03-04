//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleOffers.Exceptions
{
    public class InvalidSaleOfferException : Xeption
    {
        public InvalidSaleOfferException()
            : base(message: "Invalid sale offer, fix errors and try again.") { }
    }
}