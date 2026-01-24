//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleOffers.Exceptions
{
    public class SaleOfferValidationException : Xeption
    {
        public SaleOfferValidationException(Xeption innerException)
            : base(message: "Sale offer validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}