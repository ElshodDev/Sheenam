//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Models.Views.HostDashboards;

namespace Sheenam.Blazor.Services.Views.HostDashboards
{
    public class HostDashboardViewService : IHostDashboardViewService
    {
        private readonly IApiBroker apiBroker;

        public HostDashboardViewService(IApiBroker apiBroker) =>
            this.apiBroker = apiBroker;

        public async ValueTask<HostDashboardView> RetrieveHostDashboardViewAsync(Guid hostId)
        {
            return await this.apiBroker.GetHostDashboardByIdAsync(hostId);
        }
    }
}