//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Properties;
using Sheenam.Api.Models.Foundations.Users;
using System;

namespace Sheenam.Api.Models.Foundations.SavedSearches
{
    public class SavedSearch
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int? MinBedrooms { get; set; }
        public int? MaxBedrooms { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public ListingType? ListingType { get; set; }
        public PropertyType? PropertyType { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public User User { get; set; }
    }
}