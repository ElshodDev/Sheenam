//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.HomeRequests;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string HomeRequestsRelativeUrl = "api/homerequests";

        public async ValueTask<HomeRequest> PostHomeRequestAsync(HomeRequest homeRequest) =>
            await PostAsync(HomeRequestsRelativeUrl, homeRequest);

        public async ValueTask<List<HomeRequest>> GetAllHomeRequestsAsync() =>
            await GetAsync<List<HomeRequest>>(HomeRequestsRelativeUrl);

        public async ValueTask<HomeRequest> GetHomeRequestByIdAsync(Guid homeRequestId) =>
            await GetAsync<HomeRequest>($"{HomeRequestsRelativeUrl}/{homeRequestId}");

        public async ValueTask<HomeRequest> PutHomeRequestAsync(HomeRequest homeRequest) =>
            await PutAsync($"{HomeRequestsRelativeUrl}/{homeRequest.Id}", homeRequest);

        public async ValueTask<HomeRequest> DeleteHomeRequestByIdAsync(Guid homeRequestId) =>
            await DeleteAsync<HomeRequest>($"{HomeRequestsRelativeUrl}/{homeRequestId}");
    }
}