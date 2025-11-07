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

             return await this.storageBroker.InsertHostAsync(host);
         });

        public IQueryable<Host> RetrieveAllHosts() =>
            this.storageBroker.SelectAllHosts();

        public async ValueTask<Host> RetrieveHostByIdAsync(Guid Id)
        {
            Host maybeHost = await this.storageBroker.SelectHostByIdAsync(Id);

            if (maybeHost is null)
            {
                throw new NotFoundHostException(Id);
            }

            return maybeHost;
        }
    }

}
