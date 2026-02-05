//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Homes;
using System;
using System.Collections.Generic;

namespace Sheenam.Api.Models.Orchestrations.HostDashboards
{
    public class HostDashboard
    {
        public Guid HostId { get; set; }
        public string HostName { get; set; }
        public List<Home> Houses { get; set; }
        public int TotalHouses => Houses?.Count ?? 0;
        public decimal TotalEarnings { get; set; }
    }
}