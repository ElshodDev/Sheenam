//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Properties;
using Sheenam.Api.Models.Foundations.Users;
using System;

namespace Sheenam.Api.Models.Foundations.PropertyViews
{
    public class PropertyView
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? PropertyId { get; set; }
        public string IpAddress { get; set; }
        public DateTimeOffset ViewedDate { get; set; }

        public User User { get; set; }
        public Property Property { get; set; }
    }
}