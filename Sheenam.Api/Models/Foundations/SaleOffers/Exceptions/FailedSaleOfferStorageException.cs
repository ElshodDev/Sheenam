//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleOffers.Exceptions
{
    public class FailedSaleOfferStorageException : Xeption
    {
        public FailedSaleOfferStorageException(Exception innerException)
            : base(message: "Failed sale offer storage error occurred, contact support.",
                  innerException)
        { }
    }
}