//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleOffers.Exceptions
{
    public class NotFoundSaleOfferException : Xeption
    {
        public NotFoundSaleOfferException(Guid saleOfferId)
            : base(message: $"Sale offer not found with id: {saleOfferId}.") { }
    }
}