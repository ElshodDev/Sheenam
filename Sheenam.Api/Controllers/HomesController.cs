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
    }
}
