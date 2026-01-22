//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;

namespace Sheenam.Api.Models.Foundations.SaleOffers
{
    public class SaleOffer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PropertySaleId { get; set; }
        public Guid BuyerId { get; set; }
        public decimal OfferPrice { get; set; }
        public string Message { get; set; }
        public SaleOfferStatus Status { get; set; } = SaleOfferStatus.Pending;
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ResponseDate { get; set; }
        public string RejectionReason { get; set; }
    }
}