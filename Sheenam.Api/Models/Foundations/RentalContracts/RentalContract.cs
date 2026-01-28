//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;

namespace Sheenam.Api.Models.Foundations.RentalContracts
{
    public class RentalContract
    {
        public Guid Id { get; set; }
        public Guid HomeRequestId { get; set; }
        public Guid GuestId { get; set; }
        public Guid HostId { get; set; }
        public Guid HomeId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal SecurityDeposit { get; set; }
        public string Terms { get; set; }
        public ContractStatus Status { get; set; } = ContractStatus.Active;
        public DateTimeOffset SignedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}