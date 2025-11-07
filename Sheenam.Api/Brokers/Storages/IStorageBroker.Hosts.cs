//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Hosts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Host> InsertHostAsync(Host host);
        ValueTask<Host> SelectHostByIdAsync(Guid id);
        IQueryable<Host> SelectAllHosts();
        ValueTask<Host> UpdateHostAsync(Host host);
        ValueTask<Host> DeleteHostAsync(Host someHost);
    }
}
