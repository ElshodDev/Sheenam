//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
                return InternalServerError(rentalContractDependencyException);
            }
            catch (RentalContractServiceException rentalContractServiceException)
            {
                return InternalServerError(rentalContractServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<RentalContract>> GetAllRentalContracts()
        {
            try
            {
                IQueryable<RentalContract> retrievedRentalContracts =
                    this.rentalContractService.RetrieveAllRentalContracts();

                return Ok(retrievedRentalContracts);
            }
            catch (RentalContractDependencyException rentalContractDependencyException)
            {
                return InternalServerError(rentalContractDependencyException);
            }
            catch (RentalContractServiceException rentalContractServiceException)
            {
                return InternalServerError(rentalContractServiceException);
            }
        }
    }
}