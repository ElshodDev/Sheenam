//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.SavedSearches;
using Sheenam.Api.Models.Foundations.SavedSearches.Exceptions;
using Sheenam.Api.Services.Foundations.SavedSearches;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SavedSearchesController : RESTFulController
    {
        private readonly ISavedSearchService savedSearchService;

        public SavedSearchesController(ISavedSearchService savedSearchService) =>
            this.savedSearchService = savedSearchService;

        [HttpPost]
        public async ValueTask<ActionResult<SavedSearch>> PostSavedSearchAsync(
            SavedSearch savedSearch)
        {
            try
            {
                SavedSearch addedSavedSearch =
                    await this.savedSearchService.AddSavedSearchAsync(savedSearch);

                return Created(addedSavedSearch);
            }
            catch (SavedSearchValidationException savedSearchValidationException)
            {
                return BadRequest(savedSearchValidationException.InnerException);
            }
            catch (SavedSearchDependencyValidationException
                savedSearchDependencyValidationException)
                when (savedSearchDependencyValidationException.InnerException
                    is AlreadyExistsSavedSearchException)
            {
                return Conflict(savedSearchDependencyValidationException.InnerException);
            }
            catch (SavedSearchDependencyValidationException
                savedSearchDependencyValidationException)
            {
                return BadRequest(savedSearchDependencyValidationException.InnerException);
            }
            catch (SavedSearchDependencyException savedSearchDependencyException)
            {
                return InternalServerError(savedSearchDependencyException);
            }
            catch (SavedSearchServiceException savedSearchServiceException)
            {
                return InternalServerError(savedSearchServiceException);
            }
        }

        [HttpGet("user/{userId}")]
        public ActionResult<IQueryable<SavedSearch>> GetSavedSearchesByUserId(Guid userId)
        {
            try
            {
                IQueryable<SavedSearch> savedSearches =
                    this.savedSearchService.RetrieveSavedSearchesByUserId(userId);

                return Ok(savedSearches);
            }
            catch (SavedSearchDependencyException savedSearchDependencyException)
            {
                return InternalServerError(savedSearchDependencyException);
            }
            catch (SavedSearchServiceException savedSearchServiceException)
            {
                return InternalServerError(savedSearchServiceException);
            }
        }

        [HttpGet("{savedSearchId}")]
        public async ValueTask<ActionResult<SavedSearch>> GetSavedSearchByIdAsync(
            Guid savedSearchId)
        {
            try
            {
                SavedSearch savedSearch =
                    await this.savedSearchService.RetrieveSavedSearchByIdAsync(savedSearchId);

                return Ok(savedSearch);
            }
            catch (SavedSearchValidationException savedSearchValidationException)
                when (savedSearchValidationException.InnerException
                    is NotFoundSavedSearchException)
            {
                return NotFound(savedSearchValidationException.InnerException);
            }
            catch (SavedSearchValidationException savedSearchValidationException)
            {
                return BadRequest(savedSearchValidationException.InnerException);
            }
            catch (SavedSearchDependencyException savedSearchDependencyException)
            {
                return InternalServerError(savedSearchDependencyException);
            }
            catch (SavedSearchServiceException savedSearchServiceException)
            {
                return InternalServerError(savedSearchServiceException);
            }
        }

        [HttpDelete("{savedSearchId}")]
        public async ValueTask<ActionResult<SavedSearch>> DeleteSavedSearchByIdAsync(
            Guid savedSearchId)
        {
            try
            {
                SavedSearch deletedSavedSearch =
                    await this.savedSearchService.RemoveSavedSearchByIdAsync(savedSearchId);

                return Ok(deletedSavedSearch);
            }
            catch (SavedSearchValidationException savedSearchValidationException)
                when (savedSearchValidationException.InnerException
                    is NotFoundSavedSearchException)
            {
                return NotFound(savedSearchValidationException.InnerException);
            }
            catch (SavedSearchValidationException savedSearchValidationException)
            {
                return BadRequest(savedSearchValidationException.InnerException);
            }
            catch (SavedSearchDependencyException savedSearchDependencyException)
            {
                return InternalServerError(savedSearchDependencyException);
            }
            catch (SavedSearchServiceException savedSearchServiceException)
            {
                return InternalServerError(savedSearchServiceException);
            }
        }
    }
}
