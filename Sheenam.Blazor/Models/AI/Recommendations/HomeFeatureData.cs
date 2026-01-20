//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.ML.Data;

namespace Sheenam.Blazor.Models.AI.Recommendations
{
    public class HomeFeatureData
    {
        public float Price { get; set; }
        public float Bedrooms { get; set; }
        public float Bathrooms { get; set; }
        public float Area { get; set; }
        public float IsPetAllowed { get; set; }
        public float IsShared { get; set; }
        public float TypeScore { get; set; }
        public float Label { get; set; }
    }

    public class HomeScorePrediction
    {
        [ColumnName("Score")]
        public float PredictedScore { get; set; }
    }
}
