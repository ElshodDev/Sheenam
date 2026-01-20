
//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;

namespace Sheenam.Blazor.Models.AI.Recommendations
{
    public class HomeRecommendation
    {
        public Guid HomeId { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public double Area { get; set; }
        public bool IsPetAllowed { get; set; }
        public bool IsShared { get; set; }
        public string Type { get; set; }

        public double MatchScore { get; set; }
        public string MatchReason { get; set; }
        public int Rank { get; set; }
    }
}
