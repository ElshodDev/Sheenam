//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;

namespace Sheenam.Api.Models.Foundations.SaleTransactions
{
    public class SaleTransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PropertySaleId { get; set; }
        public Guid SellerId { get; set; }
        public Guid BuyerId { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public string ContractDocument { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}