//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;
using Sheenam.Api.Models.Foundations.Hosts;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using Sheenam.Api.Services.Foundations.Hosts;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HostsController : RESTFulController
    {
        private readonly IHostService hostService;

        public HostsController(IHostService hostService)
        {
            this.hostService = hostService;
        }
        [HttpPost]
        public async ValueTask<ActionResult<Host>> PostHostAsync(Host host)
        {
            try
            {
                Host postedHost = await this.hostService.AddHostAsync(host);

                return Created(postedHost);
            }
            catch (HostValidationException hostValidationException)
            {
                return BadRequest(hostValidationException.InnerException);
            }
            catch (HostDependencyValidationException hostDependencyValidationException)
             when (hostDependencyValidationException.InnerException is AlreadyExistHomeException)
            {
                return Conflict(hostDependencyValidationException.InnerException);
            }
            catch (HostDependencyValidationException hostDependecyValidationException)
            {
                return BadRequest(hostDependecyValidationException.InnerException);
            }
            catch (HostDependencyException hostDependecyException)
            {
                return InternalServerError(hostDependecyException.InnerException);
            }
            catch (HostServiceException hostServiceException)
            {
                return InternalServerError(hostServiceException.InnerException);
            }
        }
    }
}
