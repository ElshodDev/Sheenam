//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Orchestrations.HostDashboards;
using System;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Orchestrations.HostDashboards
{
    public interface IHostDashboardOrchestrationService
    {
        ValueTask<HostDashboard> RetrieveHostDashboardDetailsAsync(Guid hostId);
    }
}