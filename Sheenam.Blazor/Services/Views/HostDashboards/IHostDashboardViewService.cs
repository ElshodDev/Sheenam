//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Views.HostDashboards;

namespace Sheenam.Blazor.Services.Views.HostDashboards
{
    public interface IHostDashboardViewService
    {
        ValueTask<HostDashboardView> RetrieveHostDashboardViewAsync(Guid hostId);
    }
}