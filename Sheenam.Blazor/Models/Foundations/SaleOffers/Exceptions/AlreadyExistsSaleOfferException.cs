//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleOffers.Exceptions
{
    public class AlreadyExistsSaleOfferException : Xeption
    {
        public AlreadyExistsSaleOfferException(Xeption innerException)
            : base(message: "Sale offer with the same id already exists.", innerException) { }
    }
}