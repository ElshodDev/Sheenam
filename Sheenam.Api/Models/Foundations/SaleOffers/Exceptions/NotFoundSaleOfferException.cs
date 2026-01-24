//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleOffers.Exceptions
{
    public class NotFoundSaleOfferException : Xeption
    {
        public NotFoundSaleOfferException(Guid saleOfferId)
            : base(message: $"Couldn't find sale offer with id: {saleOfferId}.")
        { }
    }
}