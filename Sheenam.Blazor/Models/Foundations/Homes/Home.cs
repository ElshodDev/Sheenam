//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================
namespace Sheenam.Blazor.Models.Foundations.Homes
{
    public class Home
    {
        public Guid Id { get; set; }
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
        public ListingType ListingType { get; set; }
        public decimal? MonthlyRent { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? SecurityDeposit { get; set; }
        public string ImageUrls { get; set; }
        public bool IsFeatured { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}