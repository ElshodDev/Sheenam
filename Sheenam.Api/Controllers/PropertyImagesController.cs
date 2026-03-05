//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.PropertyImages;
using Sheenam.Api.Models.Foundations.PropertyImages.Exceptions;
using Sheenam.Api.Services.Foundations.PropertyImages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PropertyImagesController : RESTFulController
    {
        private readonly IPropertyImageService propertyImageService;

        public PropertyImagesController(IPropertyImageService propertyImageService) =>
            this.propertyImageService = propertyImageService;

        [HttpPost]
        [Authorize(Roles = "Host,Admin")]
        public async ValueTask<ActionResult<PropertyImage>> PostPropertyImageAsync(
            PropertyImage propertyImage)
        {
            try
            {
                PropertyImage addedPropertyImage =
                    await this.propertyImageService.AddPropertyImageAsync(propertyImage);

                return Created(addedPropertyImage);
            }
            catch (PropertyImageValidationException propertyImageValidationException)
            {
                return BadRequest(propertyImageValidationException.InnerException);
            }
            catch (PropertyImageDependencyValidationException
                propertyImageDependencyValidationException)
                when (propertyImageDependencyValidationException.InnerException
                    is AlreadyExistsPropertyImageException)
            {
                return Conflict(propertyImageDependencyValidationException.InnerException);
            }
            catch (PropertyImageDependencyValidationException
                propertyImageDependencyValidationException)
            {
                return BadRequest(propertyImageDependencyValidationException.InnerException);
            }
            catch (PropertyImageDependencyException propertyImageDependencyException)
            {
                return InternalServerError(propertyImageDependencyException);
            }
            catch (PropertyImageServiceException propertyImageServiceException)
            {
                return InternalServerError(propertyImageServiceException);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IQueryable<PropertyImage>> GetAllPropertyImages()
        {
            try
            {
                IQueryable<PropertyImage> propertyImages =
                    this.propertyImageService.RetrieveAllPropertyImages();

                return Ok(propertyImages);
            }
            catch (PropertyImageDependencyException propertyImageDependencyException)
            {
                return InternalServerError(propertyImageDependencyException);
            }
            catch (PropertyImageServiceException propertyImageServiceException)
            {
                return InternalServerError(propertyImageServiceException);
            }
        }

        [HttpGet("property/{propertyId}")]
        [AllowAnonymous]
        public ActionResult<IQueryable<PropertyImage>> GetPropertyImagesByPropertyId(
            Guid propertyId)
        {
            try
            {
                IQueryable<PropertyImage> propertyImages =
                    this.propertyImageService.RetrievePropertyImagesByPropertyId(propertyId);

                return Ok(propertyImages);
            }
            catch (PropertyImageDependencyException propertyImageDependencyException)
            {
                return InternalServerError(propertyImageDependencyException);
            }
            catch (PropertyImageServiceException propertyImageServiceException)
            {
                return InternalServerError(propertyImageServiceException);
            }
        }

        [HttpGet("{propertyImageId}")]
        [AllowAnonymous]
        public async ValueTask<ActionResult<PropertyImage>> GetPropertyImageByIdAsync(
            Guid propertyImageId)
        {
            try
            {
                PropertyImage propertyImage =
                    await this.propertyImageService.RetrievePropertyImageByIdAsync(propertyImageId);

                return Ok(propertyImage);
            }
            catch (PropertyImageValidationException propertyImageValidationException)
                when (propertyImageValidationException.InnerException
                    is NotFoundPropertyImageException)
            {
                return NotFound(propertyImageValidationException.InnerException);
            }
            catch (PropertyImageValidationException propertyImageValidationException)
            {
                return BadRequest(propertyImageValidationException.InnerException);
            }
            catch (PropertyImageDependencyException propertyImageDependencyException)
            {
                return InternalServerError(propertyImageDependencyException);
            }
            catch (PropertyImageServiceException propertyImageServiceException)
            {
                return InternalServerError(propertyImageServiceException);
            }
        }

        [HttpPut("{propertyImageId}")]
        [Authorize(Roles = "Host,Admin")]
        public async ValueTask<ActionResult<PropertyImage>> PutPropertyImageAsync(
            Guid propertyImageId, PropertyImage propertyImage)
        {
            try
            {
                propertyImage.Id = propertyImageId;
                PropertyImage modifiedPropertyImage =
                    await this.propertyImageService.ModifyPropertyImageAsync(propertyImage);

                return Ok(modifiedPropertyImage);
            }
            catch (PropertyImageValidationException propertyImageValidationException)
                when (propertyImageValidationException.InnerException
                    is NotFoundPropertyImageException)
            {
                return NotFound(propertyImageValidationException.InnerException);
            }
            catch (PropertyImageValidationException propertyImageValidationException)
            {
                return BadRequest(propertyImageValidationException.InnerException);
            }
            catch (PropertyImageDependencyValidationException
                propertyImageDependencyValidationException)
            {
                return BadRequest(propertyImageDependencyValidationException.InnerException);
            }
            catch (PropertyImageDependencyException propertyImageDependencyException)
            {
                return InternalServerError(propertyImageDependencyException);
            }
            catch (PropertyImageServiceException propertyImageServiceException)
            {
                return InternalServerError(propertyImageServiceException);
            }
        }

        [HttpDelete("{propertyImageId}")]
        [Authorize(Roles = "Host,Admin")]
        public async ValueTask<ActionResult<PropertyImage>> DeletePropertyImageByIdAsync(
            Guid propertyImageId)
        {
            try
            {
                PropertyImage deletedPropertyImage =
                    await this.propertyImageService.RemovePropertyImageByIdAsync(propertyImageId);

                return Ok(deletedPropertyImage);
            }
            catch (PropertyImageValidationException propertyImageValidationException)
                when (propertyImageValidationException.InnerException
                    is NotFoundPropertyImageException)
            {
                return NotFound(propertyImageValidationException.InnerException);
            }
            catch (PropertyImageValidationException propertyImageValidationException)
            {
                return BadRequest(propertyImageValidationException.InnerException);
            }
            catch (PropertyImageDependencyException propertyImageDependencyException)
            {
                return InternalServerError(propertyImageDependencyException);
            }
            catch (PropertyImageServiceException propertyImageServiceException)
            {
                return InternalServerError(propertyImageServiceException);
            }
        }
    }
}
