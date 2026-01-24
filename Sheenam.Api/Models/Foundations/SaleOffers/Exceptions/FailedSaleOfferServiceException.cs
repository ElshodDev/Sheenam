//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleOffers.Exceptions
{
    public class FailedSaleOfferServiceException : Xeption
    {
        public FailedSaleOfferServiceException(Exception innerException)
            : base(message: "Failed sale offer service error occurred, contact support.",
                  innerException)
        { }
    }
}