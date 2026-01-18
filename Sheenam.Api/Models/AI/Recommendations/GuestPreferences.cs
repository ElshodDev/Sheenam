//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

namespace Sheenam.Api.Models.AI.Recommendations
{
    public class GuestPreferences
    {
        public decimal MaxPrice { get; set; }
        public int MinBedrooms { get; set; }
        public int? MinBathrooms { get; set; }
        public double? MinArea { get; set; }
        public bool NeedsPetAllowed { get; set; }
        public bool PreferPrivate { get; set; }
        public string PreferredType { get; set; }
        public string PreferredLocation { get; set; }
    }
}
