//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Views.HostDashboards;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string HostDashboardsRelativeUrl = "api/hostdashboards";

        public async ValueTask<HostDashboardView> GetHostDashboardByIdAsync(Guid hostId) =>
            await this.GetContentAsync<HostDashboardView>($"{HostDashboardsRelativeUrl}/{hostId}");
    }
}