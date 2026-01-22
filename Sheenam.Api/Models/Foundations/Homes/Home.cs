//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;

namespace Sheenam.Api.Models.Foundations.Homes
{
    public class Home
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid HostId { get; set; }
        public string Address { get; set; }
        public string AdditionalInfo { get; set; }
        public bool IsVacant { get; set; }
        public bool IsPetAllowed { get; set; }
        public bool IsShared { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public double Area { get; set; }
        public HouseType Type { get; set; }
        public ListingType ListingType { get; set; } = ListingType.ForRent;
        public decimal? MonthlyRent { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? SecurityDeposit { get; set; }
        public string ImageUrls { get; set; } // JSON string
        public bool IsFeatured { get; set; }
        public DateTimeOffset ListedDate { get; set; }
        [Obsolete("Use MonthlyRent instead")]
        public decimal Price { get; set; }
    }
}