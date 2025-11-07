//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Hosts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public interface IHostService
    {
        ValueTask<Host> AddHostAsync(Host someHost);
        ValueTask<Host> RetrieveHostByIdAsync(Guid Id);
        IQueryable<Host> RetrieveAllHosts();
        ValueTask<Host> ModifyHostAsync(Host someHost);
        ValueTask<Host> RemoveHostByIdAsync(Guid Id);
    }
}
