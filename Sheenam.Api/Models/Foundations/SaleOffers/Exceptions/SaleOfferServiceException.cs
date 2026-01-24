//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleOffers.Exceptions
{
    public class SaleOfferServiceException : Xeption
    {
        public SaleOfferServiceException(Xeption innerException)
            : base(message: "Sale offer service error occurred, contact support.",
                  innerException)
        { }
    }
}