//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.HomeRequests;
using Sheenam.Blazor.Models.Foundations.HomeRequests;

namespace Sheenam.Blazor.Services
{
    public class HomeRequestService
    {
        private readonly HttpClient httpClient;
        private const string BaseUrl = "api/homerequests";

        public HomeRequestService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<HomeRequest>> GetAllHomeRequestsAsync()
        {
            return await this.httpClient.GetFromJsonAsync<List<HomeRequest>>(BaseUrl);
        }

        public async Task<HomeRequest> GetHomeRequestByIdAsync(Guid id)
        {
            return await this.httpClient.GetFromJsonAsync<HomeRequest>($"{BaseUrl}/{id}");
        }

        public async Task<List<HomeRequest>> GetHomeRequestsByStatusAsync(HomeRequestStatus status)
        {
            return await this.httpClient.GetFromJsonAsync<List<HomeRequest>>($"{BaseUrl}/status/{status}");
        }

        public async Task<HomeRequest> ApproveHomeRequestAsync(Guid id)
        {
            var response = await this.httpClient.PostAsync($"{BaseUrl}/{id}/approve", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<HomeRequest>();
        }

        public async Task<HomeRequest> RejectHomeRequestAsync(Guid id, string rejectionReason = null)
        {
            var url = $"{BaseUrl}/{id}/reject";

            if (!string.IsNullOrWhiteSpace(rejectionReason))
            {
                url += $"?rejectionReason={Uri.EscapeDataString(rejectionReason)}";
            }

            var response = await this.httpClient.PostAsync(url, null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<HomeRequest>();
        }

        public async Task<HomeRequest> CancelHomeRequestAsync(Guid id)
        {
            var response = await this.httpClient.PostAsync($"{BaseUrl}/{id}/cancel", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<HomeRequest>();
        }
    }
}