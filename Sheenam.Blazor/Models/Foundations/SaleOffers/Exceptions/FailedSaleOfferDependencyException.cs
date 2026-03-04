//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleOffers.Exceptions
{
    public class FailedSaleOfferDependencyException : Xeption
    {
        public FailedSaleOfferDependencyException(Exception innerException)
            : base(message: "Failed sale offer dependency error occurred.", innerException) { }
    }
}