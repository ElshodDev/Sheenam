//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string HostsRelativeUrl = "api/hosts";

        public async ValueTask<HostModel> PostHostAsync(HostModel host) =>
            await PostAsync(HostsRelativeUrl, host);

        public async ValueTask<List<HostModel>> GetAllHostsAsync() =>
            await GetAsync<List<HostModel>>(HostsRelativeUrl);

        public async ValueTask<HostModel> GetHostByIdAsync(Guid hostId) =>
            await GetAsync<HostModel>($"{HostsRelativeUrl}/{hostId}");

        public async ValueTask<HostModel> PutHostAsync(HostModel host) =>
            await PutAsync($"{HostsRelativeUrl}/{host.Id}", host);

        public async ValueTask<HostModel> DeleteHostByIdAsync(Guid hostId) =>
            await DeleteAsync<HostModel>($"{HostsRelativeUrl}/{hostId}");
    }
}