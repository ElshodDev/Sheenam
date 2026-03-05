//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;
using Sheenam.Api.Services.Foundations.Homes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HomesController : RESTFulController
    {
        private readonly IHomeService homeService;

        public HomesController(IHomeService homeService) =>
            this.homeService = homeService;

        [HttpPost]
        public async ValueTask<ActionResult<Home>> PostHomeAsync(Home home)
        {
            try
            {
                Home postedHome = await this.homeService.AddHomeAsync(home);

                return Created(postedHome);
            }
            catch (HomeValidationException homeValidationException)
            {
                return BadRequest(homeValidationException.InnerException);
            }
            catch (HomeDependencyValidationException homeDependencyValidationException)
                when (homeDependencyValidationException.InnerException is AlreadyExistHomeException)
            {
                return Conflict(homeDependencyValidationException.InnerException);
            }
            catch (HomeDependencyValidationException homeDependencyValidationException)
            {
                return BadRequest(homeDependencyValidationException.InnerException);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return InternalServerError(homeDependencyException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Home>> GetAllHomes()
        {
            try
            {
                IQueryable<Home> retrievedHomes = this.homeService.RetrieveAllHomes();

                return Ok(retrievedHomes);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return InternalServerError(homeDependencyException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException);
            }
        }

        [HttpGet("{homeId}")]
        public async ValueTask<ActionResult<Home>> GetHomeByIdAsync(Guid homeId)
        {
            try
            {
                Home retrievedHome = await this.homeService.RetrieveHomeByIdAsync(homeId);

                return Ok(retrievedHome);
            }
            catch (HomeValidationException homeValidationException)
                when (homeValidationException.InnerException is NotFoundHomeException)
            {
                return NotFound(homeValidationException.InnerException);
            }
            catch (HomeValidationException homeValidationException)
            {
                return BadRequest(homeValidationException.InnerException);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return InternalServerError(homeDependencyException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException);
            }
        }

        [HttpPut("{homeId}")]
        public async ValueTask<ActionResult<Home>> PutHomeAsync(Guid homeId, Home home)
        {
            try
            {
                home.Id = homeId;
                Home modifiedHome = await this.homeService.ModifyHomeAsync(home);

                return Ok(modifiedHome);
            }
            catch (HomeValidationException homeValidationException)
                when (homeValidationException.InnerException is NotFoundHomeException)
            {
                return NotFound(homeValidationException.InnerException);
            }
            catch (HomeValidationException homeValidationException)
            {
                return BadRequest(homeValidationException.InnerException);
            }
            catch (HomeDependencyValidationException homeDependencyValidationException)
            {
                return BadRequest(homeDependencyValidationException.InnerException);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return InternalServerError(homeDependencyException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException);
            }
        }

        [HttpDelete("{homeId}")]
        public async ValueTask<ActionResult<Home>> DeleteHomeByIdAsync(Guid homeId)
        {
            try
            {
                Home deletedHome = await this.homeService.RemoveHomeByIdAsync(homeId);

                return Ok(deletedHome);
            }
            catch (HomeValidationException homeValidationException)
                when (homeValidationException.InnerException is NotFoundHomeException)
            {
                return NotFound(homeValidationException.InnerException);
            }
            catch (HomeValidationException homeValidationException)
            {
                return BadRequest(homeValidationException.InnerException);
            }
            catch (HomeDependencyValidationException homeDependencyValidationException)
            {
                return BadRequest(homeDependencyValidationException.InnerException);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return InternalServerError(homeDependencyException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException);
            }
        }
    }
}
