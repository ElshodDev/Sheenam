//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.RentalContracts;
using Sheenam.Api.Models.Foundations.RentalContracts.Exceptions;
using Sheenam.Api.Services.Foundations.RentalContracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalContractsController : RESTFulController
    {
        private readonly IRentalContractService rentalContractService;

        public RentalContractsController(IRentalContractService rentalContractService) =>
            this.rentalContractService = rentalContractService;

        [HttpPost]
        public async ValueTask<ActionResult<RentalContract>> PostRentalContractAsync(RentalContract rentalContract)
        {
            try
            {
                RentalContract postedRentalContract =
                    await this.rentalContractService.AddRentalContractAsync(rentalContract);

                return Created(postedRentalContract);
            }
            catch (RentalContractValidationException rentalContractValidationException)
            {
                return BadRequest(rentalContractValidationException.InnerException);
            }
            catch (RentalContractDependencyValidationException rentalContractDependencyValidationException)
                when (rentalContractDependencyValidationException.InnerException is AlreadyExistsRentalContractException)
            {
                return Conflict(rentalContractDependencyValidationException.InnerException);
            }
            catch (RentalContractDependencyValidationException rentalContractDependencyValidationException)
            {
                return BadRequest(rentalContractDependencyValidationException.InnerException);
            }
            catch (RentalContractDependencyException rentalContractDependencyException)
            {
                return InternalServerError(rentalContractDependencyException.InnerException);
            }
            catch (RentalContractServiceException rentalContractServiceException)
            {
                return InternalServerError(rentalContractServiceException.InnerException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<RentalContract>> GetAllRentalContracts()
        {
            try
            {
                IQueryable<RentalContract> allRentalContracts =
                    this.rentalContractService.RetrieveAllRentalContracts();

                return Ok(allRentalContracts);
            }
            catch (RentalContractDependencyException rentalContractDependencyException)
            {
                return InternalServerError(rentalContractDependencyException.InnerException);
            }
            catch (RentalContractServiceException rentalContractServiceException)
            {
                return InternalServerError(rentalContractServiceException.InnerException);
            }
        }

        [HttpGet("{rentalContractId}")]
        public async ValueTask<ActionResult<RentalContract>> GetRentalContractByIdAsync(Guid rentalContractId)
        {
            try
            {
                RentalContract retrievedRentalContract =
                    await this.rentalContractService.RetrieveRentalContractByIdAsync(rentalContractId);

                return Ok(retrievedRentalContract);
            }
            catch (RentalContractValidationException rentalContractValidationException)
                when (rentalContractValidationException.InnerException is NotFoundRentalContractException)
            {
                return NotFound(rentalContractValidationException.InnerException);
            }
            catch (RentalContractValidationException rentalContractValidationException)
            {
                return BadRequest(rentalContractValidationException.InnerException);
            }
            catch (RentalContractDependencyException rentalContractDependencyException)
            {
                return InternalServerError(rentalContractDependencyException.InnerException);
            }
            catch (RentalContractServiceException rentalContractServiceException)
            {
                return InternalServerError(rentalContractServiceException.InnerException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<RentalContract>> PutRentalContractAsync(RentalContract rentalContract)
        {
            try
            {
                RentalContract modifiedRentalContract =
                    await this.rentalContractService.ModifyRentalContractAsync(rentalContract);

                return Ok(modifiedRentalContract);
            }
            catch (RentalContractValidationException rentalContractValidationException)
                when (rentalContractValidationException.InnerException is NotFoundRentalContractException)
            {
                return NotFound(rentalContractValidationException.InnerException);
            }
            catch (RentalContractValidationException rentalContractValidationException)
            {
                return BadRequest(rentalContractValidationException.InnerException);
            }
            catch (RentalContractDependencyException rentalContractDependencyException)
            {
                return InternalServerError(rentalContractDependencyException.InnerException);
            }
            catch (RentalContractServiceException rentalContractServiceException)
            {
                return InternalServerError(rentalContractServiceException.InnerException);
            }
        }

        [HttpDelete("{rentalContractId}")]
        public async ValueTask<ActionResult<RentalContract>> DeleteRentalContractByIdAsync(Guid rentalContractId)
        {
            try
            {
                RentalContract deletedRentalContract =
                    await this.rentalContractService.RemoveRentalContractByIdAsync(rentalContractId);

                return Ok(deletedRentalContract);
            }
            catch (RentalContractValidationException rentalContractValidationException)
                when (rentalContractValidationException.InnerException is NotFoundRentalContractException)
            {
                return NotFound(rentalContractValidationException.InnerException);
            }
            catch (RentalContractValidationException rentalContractValidationException)
            {
                return BadRequest(rentalContractValidationException.InnerException);
            }
            catch (RentalContractDependencyException rentalContractDependencyException)
            {
                return InternalServerError(rentalContractDependencyException.InnerException);
            }
            catch (RentalContractServiceException rentalContractServiceException)
            {
                return InternalServerError(rentalContractServiceException.InnerException);
            }
        }
    }
}