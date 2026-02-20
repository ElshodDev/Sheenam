//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Services.Foundations.Hosts
{
    public interface IHostService
    {
        ValueTask<HostModel> AddHostAsync(HostModel host);
        ValueTask<List<HostModel>> RetrieveAllHostsAsync();
        ValueTask<HostModel> RetrieveHostByIdAsync(Guid hostId);
        ValueTask<HostModel> ModifyHostAsync(HostModel host);
        ValueTask<HostModel> RemoveHostByIdAsync(Guid hostId);
    }
}
