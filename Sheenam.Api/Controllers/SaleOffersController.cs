//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.SaleOffers;
using Sheenam.Api.Models.Foundations.SaleOffers.Exceptions;
using Sheenam.Api.Services.Foundations.SaleOffers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SaleOffersController : RESTFulController
    {
        private readonly ISaleOfferService saleOfferService;

        public SaleOffersController(ISaleOfferService saleOfferService) =>
            this.saleOfferService = saleOfferService;

        [HttpPost]
        public async ValueTask<ActionResult<SaleOffer>> PostSaleOfferAsync(SaleOffer saleOffer)
        {
            try
            {
                SaleOffer postedSaleOffer =
                    await this.saleOfferService.AddSaleOfferAsync(saleOffer);

                return Created(postedSaleOffer);
            }
            catch (SaleOfferValidationException saleOfferValidationException)
            {
                return BadRequest(saleOfferValidationException.InnerException);
            }
            catch (SaleOfferDependencyValidationException saleOfferDependencyValidationException)
                when (saleOfferDependencyValidationException.InnerException is AlreadyExistsSaleOfferException)
            {
                return Conflict(saleOfferDependencyValidationException.InnerException);
            }
            catch (SaleOfferDependencyValidationException saleOfferDependencyValidationException)
            {
                return BadRequest(saleOfferDependencyValidationException.InnerException);
            }
            catch (SaleOfferDependencyException saleOfferDependencyException)
            {
                return InternalServerError(saleOfferDependencyException);
            }
            catch (SaleOfferServiceException saleOfferServiceException)
            {
                return InternalServerError(saleOfferServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<SaleOffer>> GetAllSaleOffers()
        {
            try
            {
                IQueryable<SaleOffer> retrievedSaleOffers =
                    this.saleOfferService.RetrieveAllSaleOffers();

                return Ok(retrievedSaleOffers);
            }
            catch (SaleOfferDependencyException saleOfferDependencyException)
            {
                return InternalServerError(saleOfferDependencyException);
            }
            catch (SaleOfferServiceException saleOfferServiceException)
            {
                return InternalServerError(saleOfferServiceException);
            }
        }

        [HttpGet("{saleOfferId}")]
        public async ValueTask<ActionResult<SaleOffer>> GetSaleOfferByIdAsync(Guid saleOfferId)
        {
            try
            {
                SaleOffer retrievedSaleOffer =
                    await this.saleOfferService.RetrieveSaleOfferByIdAsync(saleOfferId);

                return Ok(retrievedSaleOffer);
            }
            catch (SaleOfferValidationException saleOfferValidationException)
                when (saleOfferValidationException.InnerException is NotFoundSaleOfferException)
            {
                return NotFound(saleOfferValidationException.InnerException);
            }
            catch (SaleOfferValidationException saleOfferValidationException)
            {
                return BadRequest(saleOfferValidationException.InnerException);
            }
            catch (SaleOfferDependencyException saleOfferDependencyException)
            {
                return InternalServerError(saleOfferDependencyException);
            }
            catch (SaleOfferServiceException saleOfferServiceException)
            {
                return InternalServerError(saleOfferServiceException);
            }
        }

        [HttpPut("{saleOfferId}")]
        public async ValueTask<ActionResult<SaleOffer>> PutSaleOfferAsync(
            Guid saleOfferId,
            SaleOffer saleOffer)
        {
            try
            {
                saleOffer.Id = saleOfferId;

                SaleOffer modifiedSaleOffer =
                    await this.saleOfferService.ModifySaleOfferAsync(saleOffer);

                return Ok(modifiedSaleOffer);
            }
            catch (SaleOfferValidationException saleOfferValidationException)
                when (saleOfferValidationException.InnerException is NotFoundSaleOfferException)
            {
                return NotFound(saleOfferValidationException.InnerException);
            }
            catch (SaleOfferValidationException saleOfferValidationException)
            {
                return BadRequest(saleOfferValidationException.InnerException);
            }
            catch (SaleOfferDependencyValidationException saleOfferDependencyValidationException)
            {
                return BadRequest(saleOfferDependencyValidationException.InnerException);
            }
            catch (SaleOfferDependencyException saleOfferDependencyException)
            {
                return InternalServerError(saleOfferDependencyException);
            }
            catch (SaleOfferServiceException saleOfferServiceException)
            {
                return InternalServerError(saleOfferServiceException);
            }
        }

        [HttpDelete("{saleOfferId}")]
        public async ValueTask<ActionResult<SaleOffer>> DeleteSaleOfferByIdAsync(Guid saleOfferId)
        {
            try
            {
                SaleOffer deletedSaleOffer =
                    await this.saleOfferService.RemoveSaleOfferByIdAsync(saleOfferId);

                return Ok(deletedSaleOffer);
            }
            catch (SaleOfferValidationException saleOfferValidationException)
                when (saleOfferValidationException.InnerException is NotFoundSaleOfferException)
            {
                return NotFound(saleOfferValidationException.InnerException);
            }
            catch (SaleOfferValidationException saleOfferValidationException)
            {
                return BadRequest(saleOfferValidationException.InnerException);
            }
            catch (SaleOfferDependencyValidationException saleOfferDependencyValidationException)
            {
                return BadRequest(saleOfferDependencyValidationException.InnerException);
            }
            catch (SaleOfferDependencyException saleOfferDependencyException)
            {
                return InternalServerError(saleOfferDependencyException);
            }
            catch (SaleOfferServiceException saleOfferServiceException)
            {
                return InternalServerError(saleOfferServiceException);
            }
        }
    }
}