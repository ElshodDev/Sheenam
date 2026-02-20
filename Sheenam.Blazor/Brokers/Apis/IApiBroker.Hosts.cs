//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<HostModel> PostHostAsync(HostModel host);
        ValueTask<List<HostModel>> GetAllHostsAsync();
        ValueTask<HostModel> GetHostByIdAsync(Guid hostId);
        ValueTask<HostModel> PutHostAsync(HostModel host);
        ValueTask<HostModel> DeleteHostByIdAsync(Guid hostId);
    }
}