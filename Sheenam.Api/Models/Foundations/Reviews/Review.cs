//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.PropertySales;
using Sheenam.Api.Models.Foundations.Users;
using System;
using System.Text.Json.Serialization;

namespace Sheenam.Api.Models.Foundations.Reviews
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? HomeId { get; set; }
        public Guid? PropertySaleId { get; set; }
        public bool? IsPositive { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public Home Home { get; set; }

        [JsonIgnore]
        public PropertySale PropertySale { get; set; }
    }
}