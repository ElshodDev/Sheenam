//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.PropertySales;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sheenam.Api.Models.Foundations.SaleOffers
{
    public class SaleOffer
    {
        public Guid Id { get; set; }
        public Guid PropertySaleId { get; set; }
        public Guid BuyerId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal OfferPrice { get; set; }
        public string Message { get; set; }
        public SaleOfferStatus Status { get; set; } = SaleOfferStatus.Pending;
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ResponseDate { get; set; }
        public string RejectionReason { get; set; }

        public PropertySale PropertySale { get; set; }
        public Guest Buyer { get; set; }
    }
}