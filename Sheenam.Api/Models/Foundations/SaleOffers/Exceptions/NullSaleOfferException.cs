//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleOffers.Exceptions
{
    public class NullSaleOfferException : Xeption
    {
        public NullSaleOfferException()
            : base(message: "Sale offer is null.")
        { }
    }
}