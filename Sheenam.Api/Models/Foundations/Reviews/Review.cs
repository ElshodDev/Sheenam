//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;

namespace Sheenam.Api.Models.Foundations.Reviews
{
    public class Review
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid? HomeId { get; set; }
        public Guid? PropertySaleId { get; set; }
        public int Rating { get; set; } // 1-5
        public string Comment { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}