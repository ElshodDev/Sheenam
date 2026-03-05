//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Users;
using System;

namespace Sheenam.Api.Models.Foundations.Properties
{
    public class Property
    {
        public Guid Id { get; set; }
        public Guid AgentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public double Area { get; set; }
        public PropertyType Type { get; set; }
        public ListingType ListingType { get; set; }
        public decimal? MonthlyRent { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? SecurityDeposit { get; set; }
        public bool IsVacant { get; set; }
        public bool IsPetAllowed { get; set; }
        public bool IsFeatured { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public User Agent { get; set; }
    }
}