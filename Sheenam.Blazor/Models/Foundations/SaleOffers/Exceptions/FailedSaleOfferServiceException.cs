//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleOffers.Exceptions
{
    public class FailedSaleOfferServiceException : Xeption
    {
        public FailedSaleOfferServiceException(Exception innerException)
            : base(message: "Failed sale offer service error occurred.", innerException) { }
    }
}