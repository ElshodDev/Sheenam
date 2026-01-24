//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleOffers.Exceptions
{
    public class InvalidSaleOfferException : Xeption
    {
        public InvalidSaleOfferException()
            : base(message: "Sale offer is invalid.")
        { }
    }
}