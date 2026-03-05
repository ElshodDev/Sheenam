//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.PropertySales;
using Sheenam.Api.Models.Foundations.PropertySales.Exceptions;
using Sheenam.Api.Services.Foundations.PropertySales;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PropertySalesController : RESTFulController
    {
        private readonly IPropertySaleService propertySaleService;

        public PropertySalesController(IPropertySaleService propertySaleService) =>
            this.propertySaleService = propertySaleService;

        [HttpPost]
        public async ValueTask<ActionResult<PropertySale>> PostPropertySaleAsync(PropertySale propertySale)
        {
            try
            {
                PropertySale postedPropertySale =
                    await this.propertySaleService.AddPropertySaleAsync(propertySale);

                return Created(postedPropertySale);
            }
            catch (PropertySaleValidationException propertySaleValidationException)
            {
                return BadRequest(propertySaleValidationException.InnerException);
            }
            catch (PropertySaleDependencyValidationException propertySaleDependencyValidationException)
                when (propertySaleDependencyValidationException.InnerException is AlreadyExistPropertySaleException)
            {
                return Conflict(propertySaleDependencyValidationException.InnerException);
            }
            catch (PropertySaleDependencyValidationException propertySaleDependencyValidationException)
            {
                return BadRequest(propertySaleDependencyValidationException.InnerException);
            }
            catch (PropertySaleDependencyException propertySaleDependencyException)
            {
                return InternalServerError(propertySaleDependencyException);
            }
            catch (PropertySaleServiceException propertySaleServiceException)
            {
                return InternalServerError(propertySaleServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<PropertySale>> GetAllPropertySales()
        {
            try
            {
                IQueryable<PropertySale> retrievedPropertySales =
                    this.propertySaleService.RetrieveAllPropertySales();

                return Ok(retrievedPropertySales);
            }
            catch (PropertySaleDependencyException propertySaleDependencyException)
            {
                return InternalServerError(propertySaleDependencyException);
            }
            catch (PropertySaleServiceException propertySaleServiceException)
            {
                return InternalServerError(propertySaleServiceException);
            }
        }

        [HttpGet("{propertySaleId}")]
        public async ValueTask<ActionResult<PropertySale>> GetPropertySaleByIdAsync(Guid propertySaleId)
        {
            try
            {
                PropertySale retrievedPropertySale =
                    await this.propertySaleService.RetrievePropertySaleByIdAsync(propertySaleId);

                return Ok(retrievedPropertySale);
            }
            catch (PropertySaleValidationException propertySaleValidationException)
                when (propertySaleValidationException.InnerException is NotFoundPropertySaleException)
            {
                return NotFound(propertySaleValidationException.InnerException);
            }
            catch (PropertySaleValidationException propertySaleValidationException)
            {
                return BadRequest(propertySaleValidationException.InnerException);
            }
            catch (PropertySaleDependencyException propertySaleDependencyException)
            {
                return InternalServerError(propertySaleDependencyException);
            }
            catch (PropertySaleServiceException propertySaleServiceException)
            {
                return InternalServerError(propertySaleServiceException);
            }
        }

        [HttpPut("{propertySaleId}")]
        public async ValueTask<ActionResult<PropertySale>> PutPropertySaleAsync(
            Guid propertySaleId,
            PropertySale propertySale)
        {
            try
            {
                propertySale.Id = propertySaleId;

                PropertySale modifiedPropertySale =
                    await this.propertySaleService.ModifyPropertySaleAsync(propertySale);

                return Ok(modifiedPropertySale);
            }
            catch (PropertySaleValidationException propertySaleValidationException)
                when (propertySaleValidationException.InnerException is NotFoundPropertySaleException)
            {
                return NotFound(propertySaleValidationException.InnerException);
            }
            catch (PropertySaleValidationException propertySaleValidationException)
            {
                return BadRequest(propertySaleValidationException.InnerException);
            }
            catch (PropertySaleDependencyValidationException propertySaleDependencyValidationException)
            {
                return BadRequest(propertySaleDependencyValidationException.InnerException);
            }
            catch (PropertySaleDependencyException propertySaleDependencyException)
            {
                return InternalServerError(propertySaleDependencyException);
            }
            catch (PropertySaleServiceException propertySaleServiceException)
            {
                return InternalServerError(propertySaleServiceException);
            }
        }

        [HttpDelete("{propertySaleId}")]
        public async ValueTask<ActionResult<PropertySale>> DeletePropertySaleByIdAsync(Guid propertySaleId)
        {
            try
            {
                PropertySale deletedPropertySale =
                    await this.propertySaleService.RemovePropertySaleByIdAsync(propertySaleId);

                return Ok(deletedPropertySale);
            }
            catch (PropertySaleValidationException propertySaleValidationException)
                when (propertySaleValidationException.InnerException is NotFoundPropertySaleException)
            {
                return NotFound(propertySaleValidationException.InnerException);
            }
            catch (PropertySaleValidationException propertySaleValidationException)
            {
                return BadRequest(propertySaleValidationException.InnerException);
            }
            catch (PropertySaleDependencyValidationException propertySaleDependencyValidationException)
            {
                return BadRequest(propertySaleDependencyValidationException.InnerException);
            }
            catch (PropertySaleDependencyException propertySaleDependencyException)
            {
                return InternalServerError(propertySaleDependencyException);
            }
            catch (PropertySaleServiceException propertySaleServiceException)
            {
                return InternalServerError(propertySaleServiceException);
            }
        }
    }
}