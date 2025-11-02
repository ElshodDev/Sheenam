//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Http;
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
    public class HomesController : RESTFulController
    {
        private readonly IHomeService homeService;
        public HomesController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostHomeAsync(Home home)
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
            catch (HomeDependencyException homeDependecyException)
            {
                return InternalServerError(homeDependecyException.InnerException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException.InnerException);
            }
        }

        [HttpGet("{homeId}")]
        public async ValueTask<IActionResult> GetHomeByIdAsync(Guid homeId)
        {
            try
            {
                Home retrievedHome = await this.homeService.RetrieveHomeByIdAsync(homeId);
                return Ok(retrievedHome);
            }
            catch (HomeValidationException homeValidationException)
            {
                return BadRequest(homeValidationException.InnerException);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return Problem(
                    detail: homeDependencyException.InnerException.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            catch (HomeServiceException homeServiceException)
            {
                return Problem(
                    detail: homeServiceException.InnerException.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Home>> GetAllHomes()
        {
            try
            {
                IQueryable<Home> allHomes = this.homeService.RetrieveAllHomes();
                return Ok(allHomes);
            }
            catch (HomeDependencyException homeDependencyException)
            {
                return Problem(
                    detail: homeDependencyException.InnerException.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            catch (HomeServiceException homeServiceException)
            {
                return Problem(
                    detail: homeServiceException.InnerException.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{homeId}")]
        public async ValueTask<IActionResult> PutHomeAsync(Guid homeId, Home home)
        {
            try
            {
                home.Id = homeId;

                Home updatedHome = await this.homeService.ModifyHomeAsync(home);
                return Ok(updatedHome);
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
                return InternalServerError(homeDependencyException.InnerException);
            }
            catch (HomeServiceException homeServiceException)
            {
                return InternalServerError(homeServiceException.InnerException);
            }
        }

    }
}
