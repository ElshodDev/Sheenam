//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleOffers.Exceptions
{
    public class AlreadyExistsSaleOfferException : Xeption
    {
        public AlreadyExistsSaleOfferException(Exception innerException)
            : base(message: "Sale offer already exists.", innerException)
        { }
    }
}