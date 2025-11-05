//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Hosts;
using System;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public partial class HostService : IHostService
    {
        private readonly IStorageBroker storageBroker;

        public HostService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;


        public async ValueTask<Host> AddHostAsync(Host host) =>
          await  this.storageBroker.InsertHostAsync(host);

    }
}
