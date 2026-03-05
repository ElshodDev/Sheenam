//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Hosts;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public partial class HostService : IHostService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public HostService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker=loggingBroker;
        }

        public ValueTask<Host> AddHostAsync(Host host) =>
         TryCatch(async () =>
         {
             ValidateHostOnAdd(host);

             host.Id = Guid.NewGuid();

             return await this.storageBroker.InsertHostAsync(host);
         });

        public IQueryable<Host> RetrieveAllHosts() =>
            TryCatch(() => this.storageBroker.SelectAllHosts());

        public ValueTask<Host> RetrieveHostByIdAsync(Guid hostId) =>
            TryCatch(async () =>
            {
                ValidateHostId(hostId);
                Host maybeHost = await this.storageBroker.SelectHostByIdAsync(hostId);
                ValidateStorageHost(maybeHost, hostId);

                return maybeHost;
            });

        public ValueTask<Host> ModifyHostAsync(Host host) =>
            TryCatch(async () =>
            {
                ValidateHostOnModify(host);
                Host maybeHost = await this.storageBroker.SelectHostByIdAsync(host.Id);
                ValidateStorageHost(maybeHost, host.Id);

                return await this.storageBroker.UpdateHostAsync(host);
            });

        public ValueTask<Host> RemoveHostByIdAsync(Guid hostId) =>
            TryCatch(async () =>
            {
                ValidateHostId(hostId);
                Host maybeHost = await this.storageBroker.SelectHostByIdAsync(hostId);
                ValidateStorageHost(maybeHost, hostId);

                return await this.storageBroker.DeleteHostAsync(maybeHost);
            });
    }

}
