//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleOffers.Exceptions
{
    public class SaleOfferDependencyException : Xeption
    {
        public SaleOfferDependencyException(Xeption innerException)
            : base(message: "Sale offer dependency error occurred.", innerException) { }
    }
}