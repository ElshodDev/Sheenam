//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Properties;
using System;

namespace Sheenam.Api.Models.Foundations.PropertyImages
{
    public class PropertyImage
    {
        public Guid Id { get; set; }
        public Guid? PropertyId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
        public int SortOrder { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public Property Property { get; set; }
    }
}