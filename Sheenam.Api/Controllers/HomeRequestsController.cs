//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.HomeRequests;
using Sheenam.Api.Models.Foundations.HomeRequests.Exceptions;
using Sheenam.Api.Services.Foundations.HomeRequests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeRequestsController : RESTFulController
    {
        private readonly IHomeRequestService homeRequestService;

        public HomeRequestsController(IHomeRequestService homeRequestService)
        {
            this.homeRequestService = homeRequestService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<HomeRequest>> PostHomeRequestAsync(HomeRequest homeRequest)
        {
            try
            {
                HomeRequest postedHomeRequest =
                    await this.homeRequestService.AddHomeRequestAsync(homeRequest);

                return Created(postedHomeRequest);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
            when (homeRequestDependencyValidationException.InnerException is AlreadyExistsHomeRequestException)
            {
                return Conflict(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
            {
                return BadRequest(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<HomeRequest>> GetAllHomeRequests()
        {
            try
            {
                IQueryable<HomeRequest> retrievedHomeRequests =
                    this.homeRequestService.RetrieveAllHomeRequests();

                return Ok(retrievedHomeRequests);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException);
            }
        }

        [HttpGet("{homeRequestId}")]
        public async ValueTask<ActionResult<HomeRequest>> GetHomeRequestByIdAsync(
            Guid homeRequestId)
        {
            try
            {
                HomeRequest retrievedHomeRequest =
                    await this.homeRequestService.RetrieveHomeRequestByIdAsync(homeRequestId);
                return Ok(retrievedHomeRequest);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException);
            }
        }

        [HttpGet("status/{status}")]
        [HttpGet("status/{status}")]
        public ActionResult<IQueryable<HomeRequest>> GetHomeRequestsByStatus(HomeRequestStatus status)
        {
            try
            {
                IQueryable<HomeRequest> retrievedHomeRequests =
                    this.homeRequestService.RetrieveHomeRequestsByStatusAsync(status);

                return Ok(retrievedHomeRequests);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException);
            }
        }

        [HttpPut("{homeRequestId}")]
        public async ValueTask<ActionResult<HomeRequest>> PutHomeRequestAsync(
            Guid homeRequestId,
            HomeRequest homeRequest)
        {
            try
            {
                homeRequest.Id = homeRequestId;

                HomeRequest modifiedHomeRequest =
                    await this.homeRequestService.ModifyHomeRequestAsync(homeRequest);
                return Ok(modifiedHomeRequest);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
            when (homeRequestDependencyValidationException.InnerException is NotFoundHomeRequestException)
            {
                return NotFound(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
            {
                return BadRequest(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException);
            }
        }

        [HttpPost("{homeRequestId}/approve")]
        [HttpPost("{homeRequestId}/approve")]
        public async ValueTask<ActionResult<HomeRequest>> ApproveHomeRequestAsync(
            Guid homeRequestId)
        {
            try
            {
                HomeRequest approvedHomeRequest =
                    await this.homeRequestService.ApproveHomeRequestAsync(homeRequestId);

                return Ok(approvedHomeRequest);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (InvalidHomeRequestStatusException invalidStatusException)
            {
                return BadRequest(invalidStatusException);
            }
            catch (NotFoundHomeRequestException notFoundException)
            {
                return NotFound(notFoundException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException);
            }
        }

        [HttpPost("{homeRequestId}/reject")]
        public async ValueTask<ActionResult<HomeRequest>> RejectHomeRequestAsync(
            Guid homeRequestId,
            [FromQuery] string rejectionReason = null)
        {
            try
            {
                HomeRequest rejectedHomeRequest =
                    await this.homeRequestService.RejectHomeRequestAsync(
                        homeRequestId,
                        rejectionReason);

                return Ok(rejectedHomeRequest);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (InvalidHomeRequestStatusException invalidStatusException)
            {
                return BadRequest(invalidStatusException);
            }
            catch (NotFoundHomeRequestException notFoundException)
            {
                return NotFound(notFoundException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException);
            }
        }

        [HttpPost("{homeRequestId}/cancel")]
        public async ValueTask<ActionResult<HomeRequest>> CancelHomeRequestAsync(
            Guid homeRequestId)
        {
            try
            {
                HomeRequest cancelledHomeRequest =
                    await this.homeRequestService.CancelHomeRequestAsync(homeRequestId);

                return Ok(cancelledHomeRequest);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (InvalidHomeRequestStatusException invalidStatusException)
            {
                return BadRequest(invalidStatusException);
            }
            catch (NotFoundHomeRequestException notFoundException)
            {
                return NotFound(notFoundException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException);
            }
        }

        [HttpDelete("{homeRequestId}")]
        public async ValueTask<ActionResult<HomeRequest>> DeleteHomeRequestByIdAsync(
            Guid homeRequestId)
        {
            try
            {
                HomeRequest deletedHomeRequest =
                    await this.homeRequestService.RemoveHomeRequestByIdAsync(homeRequestId);
                return Ok(deletedHomeRequest);
            }
            catch (HomeRequestValidationException homeRequestValidationException)
            {
                return BadRequest(homeRequestValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
            when (homeRequestDependencyValidationException.InnerException is NotFoundHomeRequestException)
            {
                return NotFound(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyValidationException homeRequestDependencyValidationException)
            {
                return BadRequest(homeRequestDependencyValidationException.InnerException);
            }
            catch (HomeRequestDependencyException homeRequestDependencyException)
            {
                return InternalServerError(homeRequestDependencyException);
            }
            catch (HomeRequestServiceException homeRequestServiceException)
            {
                return InternalServerError(homeRequestServiceException);
            }
        }
    }
}