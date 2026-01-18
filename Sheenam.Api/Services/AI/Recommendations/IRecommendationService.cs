//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.AI.Recommendations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.AI.Recommendations
{
    public interface IRecommendationService
    {
        Task<List<HomeRecommendation>> GetRecommendedHomesAsync(
            GuestPreferences preferences,
            int topN = 10);

        Task TrainModelAsync();
    }
}
