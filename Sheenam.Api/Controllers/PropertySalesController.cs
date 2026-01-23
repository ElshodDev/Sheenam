//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
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
    public class PropertySalesController : RESTFulController
    {
        private readonly IPropertySaleService propertySaleService;

        public PropertySalesController(IPropertySaleService propertySaleService)
        {
            this.propertySaleService = propertySaleService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostPropertySaleAsync(SaleOffer propertySale)
        {
            try
            {
                SaleOffer postedPropertySale =
                    await this.propertySaleService.AddPropertySaleAsync(propertySale);

                return Created(postedPropertySale);
            }
            catch (PropertySaleValidationException propertySaleValidationException)
            {
                return BadRequest(propertySaleValidationException.InnerException);
            }
            catch (PropertySaleDependencyValidationException propertySaleDependencyValidationException)
                when (propertySaleDependencyValidationException.InnerException
                    is AlreadyExistPropertySaleException)
            {
                return Conflict(propertySaleDependencyValidationException.InnerException);
            }
            catch (PropertySaleDependencyValidationException propertySaleDependencyValidationException)
            {
                return BadRequest(propertySaleDependencyValidationException.InnerException);
            }
            catch (PropertySaleDependencyException propertySaleDependencyException)
            {
                return InternalServerError(propertySaleDependencyException.InnerException);
            }
            catch (PropertySaleServiceException propertySaleServiceException)
            {
                return InternalServerError(propertySaleServiceException.InnerException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<SaleOffer>> GetAllPropertySales()
        {
            try
            {
                IQueryable<SaleOffer> allPropertySales =
                    this.propertySaleService.RetrieveAllPropertySales();

                return Ok(allPropertySales);
            }
            catch (PropertySaleDependencyException propertySaleDependencyException)
            {
                return InternalServerError(propertySaleDependencyException.InnerException);
            }
            catch (PropertySaleServiceException propertySaleServiceException)
            {
                return InternalServerError(propertySaleServiceException.InnerException);
            }
        }

        [HttpGet("{propertySaleId}")]
        public async ValueTask<ActionResult<SaleOffer>> GetPropertySaleByIdAsync(Guid propertySaleId)
        {
            try
            {
                SaleOffer propertySale =
                    await this.propertySaleService.RetrievePropertySaleByIdAsync(propertySaleId);

                return Ok(propertySale);
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
                return InternalServerError(propertySaleDependencyException.InnerException);
            }
            catch (PropertySaleServiceException propertySaleServiceException)
            {
                return InternalServerError(propertySaleServiceException.InnerException);
            }
        }

        [HttpPut("{propertySaleId}")]
        public async ValueTask<ActionResult<SaleOffer>> PutPropertySaleAsync(
            Guid propertySaleId,
            SaleOffer propertySale)
        {
            try
            {
                propertySale.Id = propertySaleId;

                SaleOffer updatedPropertySale =
                    await this.propertySaleService.ModifyPropertySaleAsync(propertySale);

                return Ok(updatedPropertySale);
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
                return InternalServerError(propertySaleDependencyException.InnerException);
            }
            catch (PropertySaleServiceException propertySaleServiceException)
            {
                return InternalServerError(propertySaleServiceException.InnerException);
            }
        }

        [HttpDelete("{propertySaleId}")]
        public async ValueTask<ActionResult<SaleOffer>> DeletePropertySaleByIdAsync(Guid propertySaleId)
        {
            try
            {
                SaleOffer deletedPropertySale =
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
            catch (PropertySaleDependencyException propertySaleDependencyException)
            {
                return InternalServerError(propertySaleDependencyException.InnerException);
            }
            catch (PropertySaleServiceException propertySaleServiceException)
            {
                return InternalServerError(propertySaleServiceException.InnerException);
            }
        }
    }
}