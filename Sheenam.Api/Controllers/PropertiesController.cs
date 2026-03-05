//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Properties;
using Sheenam.Api.Models.Foundations.Properties.Exceptions;
using Sheenam.Api.Services.Foundations.Properties;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PropertiesController : RESTFulController
    {
        private readonly IPropertyService propertyService;

        public PropertiesController(IPropertyService propertyService) =>
            this.propertyService = propertyService;

        [HttpPost]
        [Authorize(Roles = "Host,Admin")]
        public async ValueTask<ActionResult<Property>> PostPropertyAsync(Property property)
        {
            try
            {
                Property addedProperty =
                    await this.propertyService.AddPropertyAsync(property);

                return Created(addedProperty);
            }
            catch (PropertyValidationException propertyValidationException)
            {
                return BadRequest(propertyValidationException.InnerException);
            }
            catch (PropertyDependencyValidationException propertyDependencyValidationException)
                when (propertyDependencyValidationException.InnerException
                    is AlreadyExistPropertyException)
            {
                return Conflict(propertyDependencyValidationException.InnerException);
            }
            catch (PropertyDependencyValidationException propertyDependencyValidationException)
            {
                return BadRequest(propertyDependencyValidationException.InnerException);
            }
            catch (PropertyDependencyException propertyDependencyException)
            {
                return InternalServerError(propertyDependencyException);
            }
            catch (PropertyServiceException propertyServiceException)
            {
                return InternalServerError(propertyServiceException);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IQueryable<Property>> GetAllProperties()
        {
            try
            {
                IQueryable<Property> properties =
                    this.propertyService.RetrieveAllProperties();

                return Ok(properties);
            }
            catch (PropertyDependencyException propertyDependencyException)
            {
                return InternalServerError(propertyDependencyException);
            }
            catch (PropertyServiceException propertyServiceException)
            {
                return InternalServerError(propertyServiceException);
            }
        }

        [HttpGet("{propertyId}")]
        [AllowAnonymous]
        public async ValueTask<ActionResult<Property>> GetPropertyByIdAsync(Guid propertyId)
        {
            try
            {
                Property property =
                    await this.propertyService.RetrievePropertyByIdAsync(propertyId);

                return Ok(property);
            }
            catch (PropertyValidationException propertyValidationException)
                when (propertyValidationException.InnerException is NotFoundPropertyException)
            {
                return NotFound(propertyValidationException.InnerException);
            }
            catch (PropertyValidationException propertyValidationException)
            {
                return BadRequest(propertyValidationException.InnerException);
            }
            catch (PropertyDependencyException propertyDependencyException)
            {
                return InternalServerError(propertyDependencyException);
            }
            catch (PropertyServiceException propertyServiceException)
            {
                return InternalServerError(propertyServiceException);
            }
        }

        [HttpPut("{propertyId}")]
        [Authorize(Roles = "Host,Admin")]
        public async ValueTask<ActionResult<Property>> PutPropertyAsync(
            Guid propertyId,
            Property property)
        {
            try
            {
                property.Id = propertyId;
                Property modifiedProperty =
                    await this.propertyService.ModifyPropertyAsync(property);

                return Ok(modifiedProperty);
            }
            catch (PropertyValidationException propertyValidationException)
                when (propertyValidationException.InnerException is NotFoundPropertyException)
            {
                return NotFound(propertyValidationException.InnerException);
            }
            catch (PropertyValidationException propertyValidationException)
            {
                return BadRequest(propertyValidationException.InnerException);
            }
            catch (PropertyDependencyValidationException propertyDependencyValidationException)
            {
                return BadRequest(propertyDependencyValidationException.InnerException);
            }
            catch (PropertyDependencyException propertyDependencyException)
            {
                return InternalServerError(propertyDependencyException);
            }
            catch (PropertyServiceException propertyServiceException)
            {
                return InternalServerError(propertyServiceException);
            }
        }

        [HttpDelete("{propertyId}")]
        [Authorize(Roles = "Host,Admin")]
        public async ValueTask<ActionResult<Property>> DeletePropertyByIdAsync(Guid propertyId)
        {
            try
            {
                Property deletedProperty =
                    await this.propertyService.RemovePropertyByIdAsync(propertyId);

                return Ok(deletedProperty);
            }
            catch (PropertyValidationException propertyValidationException)
                when (propertyValidationException.InnerException is NotFoundPropertyException)
            {
                return NotFound(propertyValidationException.InnerException);
            }
            catch (PropertyValidationException propertyValidationException)
            {
                return BadRequest(propertyValidationException.InnerException);
            }
            catch (PropertyDependencyException propertyDependencyException)
            {
                return InternalServerError(propertyDependencyException);
            }
            catch (PropertyServiceException propertyServiceException)
            {
                return InternalServerError(propertyServiceException);
            }
        }
    }
}