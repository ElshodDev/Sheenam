//===================================================
//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Homes;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string HomesRelativeUrl = "api/homes";

        public async ValueTask<Home> PostHomeAsync(Home home) =>
            await PostAsync(HomesRelativeUrl, home);

        public async ValueTask<List<Home>> GetAllHomesAsync() =>
            await GetAsync<List<Home>>(HomesRelativeUrl);

        public async ValueTask<Home> GetHomeByIdAsync(Guid homeId) =>
            await GetAsync<Home>($"{HomesRelativeUrl}/{homeId}");

        public async ValueTask<Home> PutHomeAsync(Home home) =>
            await PutAsync($"{HomesRelativeUrl}/{home.Id}", home);

        public async ValueTask<Home> DeleteHomeByIdAsync(Guid homeId) =>
            await DeleteAsync<Home>($"{HomesRelativeUrl}/{homeId}");
    }
}