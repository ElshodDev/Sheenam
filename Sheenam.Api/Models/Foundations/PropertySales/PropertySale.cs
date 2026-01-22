//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Homes;
using System;

namespace Sheenam.Api.Models.Foundations.PropertySales
{
    public class PropertySale
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid HostId { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public HouseType Type { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public double Area { get; set; }
        public decimal SalePrice { get; set; }
        public PropertySaleStatus Status { get; set; } = PropertySaleStatus.Available;
        public DateTimeOffset ListedDate { get; set; }
        public DateTimeOffset? SoldDate { get; set; }
        public string ImageUrls { get; set; } 
        public string LegalDocuments { get; set; }
        public bool IsFeatured { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}