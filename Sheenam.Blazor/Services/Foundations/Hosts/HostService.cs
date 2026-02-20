//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Brokers.Apis;
using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Services.Foundations.Hosts
{
    public class HostService : IHostService
    {
        private readonly IApiBroker apiBroker;

        public HostService(IApiBroker apiBroker) =>
            this.apiBroker = apiBroker;

        public async ValueTask<HostModel> AddHostAsync(HostModel host) =>
            await this.apiBroker.PostHostAsync(host);

        public async ValueTask<List<HostModel>> RetrieveAllHostsAsync() =>
            await this.apiBroker.GetAllHostsAsync();

        public async ValueTask<HostModel> RetrieveHostByIdAsync(Guid hostId) =>
            await this.apiBroker.GetHostByIdAsync(hostId);

        public async ValueTask<HostModel> ModifyHostAsync(HostModel host) =>
            await this.apiBroker.PutHostAsync(host);

        public async ValueTask<HostModel> RemoveHostByIdAsync(Guid hostId) =>
            await this.apiBroker.DeleteHostByIdAsync(hostId);
    }
}