//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleOffers.Exceptions
{
    public class SaleOfferDependencyValidationException : Xeption
    {
        public SaleOfferDependencyValidationException(Xeption innerException)
            : base(message: "Sale offer dependency validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}