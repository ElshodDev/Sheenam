//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sheenam.Api.Models.Orchestrations.HostDashboards;
using Sheenam.Api.Services.Orchestrations.HostDashboards;
using System;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // Faqat login qilganlar uchun
    public class HostDashboardsController : ControllerBase
    {
        private readonly IHostDashboardOrchestrationService hostDashboardOrchestrationService;

        public HostDashboardsController(IHostDashboardOrchestrationService hostDashboardOrchestrationService) =>
            this.hostDashboardOrchestrationService = hostDashboardOrchestrationService;

        [HttpGet("{hostId}")]
        [AllowAnonymous] // Hamma uchun ochiq, hatto login qilmaganlar uchun ham
        //[Authorize(Policy = "HostOnly")] // Faqat Role == Host bo'lganlar uchun
        public async ValueTask<ActionResult<HostDashboard>> GetHostDashboardAsync(Guid hostId)
        {
            HostDashboard hostDashboard =
                await this.hostDashboardOrchestrationService.RetrieveHostDashboardDetailsAsync(hostId);

            return Ok(hostDashboard);
        }
    }
}