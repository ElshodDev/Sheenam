//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.PropertyViews;
using Sheenam.Api.Models.Foundations.PropertyViews.Exceptions;
using Sheenam.Api.Services.Foundations.PropertyViews;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyViewsController : RESTFulController
    {
        private readonly IPropertyViewService propertyViewService;

        public PropertyViewsController(IPropertyViewService propertyViewService) =>
            this.propertyViewService = propertyViewService;

        [HttpPost]
        [AllowAnonymous]
        public async ValueTask<ActionResult<PropertyView>> PostPropertyViewAsync(
            PropertyView propertyView)
        {
            try
            {
                PropertyView addedPropertyView =
                    await this.propertyViewService.AddPropertyViewAsync(propertyView);

                return Created(addedPropertyView);
            }
            catch (PropertyViewValidationException propertyViewValidationException)
            {
                return BadRequest(propertyViewValidationException.InnerException);
            }
            catch (PropertyViewDependencyValidationException
                propertyViewDependencyValidationException)
                when (propertyViewDependencyValidationException.InnerException
                    is AlreadyExistsPropertyViewException)
            {
                return Conflict(propertyViewDependencyValidationException.InnerException);
            }
            catch (PropertyViewDependencyValidationException
                propertyViewDependencyValidationException)
            {
                return BadRequest(propertyViewDependencyValidationException.InnerException);
            }
            catch (PropertyViewDependencyException propertyViewDependencyException)
            {
                return InternalServerError(propertyViewDependencyException);
            }
            catch (PropertyViewServiceException propertyViewServiceException)
            {
                return InternalServerError(propertyViewServiceException);
            }
        }

        [HttpGet("property/{propertyId}")]
        [AllowAnonymous]
        public ActionResult<IQueryable<PropertyView>> GetPropertyViewsByPropertyId(Guid propertyId)
        {
            try
            {
                IQueryable<PropertyView> propertyViews =
                    this.propertyViewService.RetrievePropertyViewsByPropertyId(propertyId);

                return Ok(propertyViews);
            }
            catch (PropertyViewDependencyException propertyViewDependencyException)
            {
                return InternalServerError(propertyViewDependencyException);
            }
            catch (PropertyViewServiceException propertyViewServiceException)
            {
                return InternalServerError(propertyViewServiceException);
            }
        }

        [HttpGet("{propertyViewId}")]
        [Authorize]
        public async ValueTask<ActionResult<PropertyView>> GetPropertyViewByIdAsync(
            Guid propertyViewId)
        {
            try
            {
                PropertyView propertyView =
                    await this.propertyViewService.RetrievePropertyViewByIdAsync(propertyViewId);

                return Ok(propertyView);
            }
            catch (PropertyViewValidationException propertyViewValidationException)
                when (propertyViewValidationException.InnerException
                    is NotFoundPropertyViewException)
            {
                return NotFound(propertyViewValidationException.InnerException);
            }
            catch (PropertyViewValidationException propertyViewValidationException)
            {
                return BadRequest(propertyViewValidationException.InnerException);
            }
            catch (PropertyViewDependencyException propertyViewDependencyException)
            {
                return InternalServerError(propertyViewDependencyException);
            }
            catch (PropertyViewServiceException propertyViewServiceException)
            {
                return InternalServerError(propertyViewServiceException);
            }
        }

        [HttpDelete("{propertyViewId}")]
        [Authorize(Roles = "Admin")]
        public async ValueTask<ActionResult<PropertyView>> DeletePropertyViewByIdAsync(
            Guid propertyViewId)
        {
            try
            {
                PropertyView deletedPropertyView =
                    await this.propertyViewService.RemovePropertyViewByIdAsync(propertyViewId);

                return Ok(deletedPropertyView);
            }
            catch (PropertyViewValidationException propertyViewValidationException)
                when (propertyViewValidationException.InnerException
                    is NotFoundPropertyViewException)
            {
                return NotFound(propertyViewValidationException.InnerException);
            }
            catch (PropertyViewValidationException propertyViewValidationException)
            {
                return BadRequest(propertyViewValidationException.InnerException);
            }
            catch (PropertyViewDependencyException propertyViewDependencyException)
            {
                return InternalServerError(propertyViewDependencyException);
            }
            catch (PropertyViewServiceException propertyViewServiceException)
            {
                return InternalServerError(propertyViewServiceException);
            }
        }
    }
}
