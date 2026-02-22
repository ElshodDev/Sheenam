//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Services.Foundations.Hosts
{
    public partial class HostService : IHostService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public HostService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<HostModel> AddHostAsync(HostModel host) =>
            await TryCatch(async () =>
            {
                ValidateHostOnAdd(host);
                return await this.apiBroker.PostHostAsync(host);
            });

        public async ValueTask<List<HostModel>> RetrieveAllHostsAsync() =>
            await TryCatch(async () => await this.apiBroker.GetAllHostsAsync());

        public async ValueTask<HostModel> RetrieveHostByIdAsync(Guid hostId) =>
            await TryCatch(async () =>
            {
                ValidateHostId(hostId);
                return await this.apiBroker.GetHostByIdAsync(hostId);
            });

        public async ValueTask<HostModel> ModifyHostAsync(HostModel host) =>
            await TryCatch(async () =>
            {
                ValidateHostOnModify(host);
                return await this.apiBroker.PutHostAsync(host);
            });

        public async ValueTask<HostModel> RemoveHostByIdAsync(Guid hostId) =>
            await TryCatch(async () =>
            {
                ValidateHostId(hostId);
                return await this.apiBroker.DeleteHostByIdAsync(hostId);
            });
    }
}