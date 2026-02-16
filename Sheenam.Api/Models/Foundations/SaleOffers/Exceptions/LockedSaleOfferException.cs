//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleOffers.Exceptions
{
    public class LockedSaleOfferException : Xeption
    {
        public LockedSaleOfferException(Exception innerException)
            : base(message: $"Locked sale offer with id:", innerException)
        { }
    }
}
