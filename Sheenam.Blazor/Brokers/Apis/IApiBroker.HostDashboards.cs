//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Views.HostDashboards;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<HostDashboardView> GetHostDashboardByIdAsync(Guid hostId);
    }
}