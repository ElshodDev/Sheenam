//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.SaleTransactions;
using Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions;
using Sheenam.Api.Services.SaleTransactions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleTransactionsController : RESTFulController
    {
        private readonly ISaleTransactionService saleTransactionService;

        public SaleTransactionsController(ISaleTransactionService saleTransactionService) =>
              this.saleTransactionService = saleTransactionService;

        [HttpPost]
        public async ValueTask<ActionResult<SaleTransaction>> PostSaleTransactionAsync(SaleTransaction saleTransaction)
        {
            try
            {
                SaleTransaction addedSaleTransaction =
                    await this.saleTransactionService.AddSaleTransactionAsync(saleTransaction);
                return Created(addedSaleTransaction);
            }
            catch (SaleTransactionDependencyValidationException dependencyValidationException)
            when (dependencyValidationException.InnerException is AlreadyExistsSaleTransactionException)
            {
                return Conflict(dependencyValidationException.InnerException);
            }
            catch (SaleTransactionDependencyValidationException dependencyValidationException)
            {
                return BadRequest(dependencyValidationException.InnerException);
            }
            catch (SaleTransactionDependencyException dependencyException)
            {
                return InternalServerError(dependencyException.InnerException);
            }
            catch (SaleTransactionValidationException saleTransactionValidationException)
            {
                return BadRequest(saleTransactionValidationException.InnerException);
            }
            catch (SaleTransactionServiceException serviceException)
            {
                return InternalServerError(serviceException.InnerException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<SaleTransaction>> GetAllSaleTransactions()
        {
            try
            {
                IQueryable<SaleTransaction> allSaleTransactions =
                    this.saleTransactionService.RetrieveAllSaleTransactions();
                return Ok(allSaleTransactions);
            }
            catch (SaleTransactionDependencyException dependencyException)
            {
                return InternalServerError(dependencyException.InnerException);
            }
            catch (SaleTransactionServiceException serviceException)
            {
                return InternalServerError(serviceException.InnerException);
            }
        }

        [HttpGet("{saleTransactionId}")]
        public async ValueTask<ActionResult<SaleTransaction>> GetSaleTransactionByIdAsync(Guid saleTransactionId)
        {
            try
            {
                SaleTransaction retrievedSaleTransaction =
                    await this.saleTransactionService.RetrieveSaleTransactionByIdAsync(saleTransactionId);
                return Ok(retrievedSaleTransaction);
            }
            catch (SaleTransactionValidationException validationException)
            when (validationException.InnerException is NotFoundSaleTransactionException)
            {
                return NotFound(validationException.InnerException);
            }
            catch (SaleTransactionValidationException validationException)
            {
                return BadRequest(validationException.InnerException);
            }
            catch (SaleTransactionDependencyException dependencyException)
            {
                return InternalServerError(dependencyException.InnerException);
            }
            catch (SaleTransactionServiceException serviceException)
            {
                return InternalServerError(serviceException.InnerException);
            }
        }

        [HttpPut("{saleTransactionId}")]
        public async ValueTask<ActionResult<SaleTransaction>> PutSaleTransactionAsync(Guid saleTransactionId, SaleTransaction saleTransaction)
        {
            try
            {
                saleTransaction.Id = saleTransactionId;
                SaleTransaction modifiedSaleTransaction =
                await this.saleTransactionService.ModifySaleTransactionAsync(saleTransaction);
                return Ok(modifiedSaleTransaction);
            }
            catch (SaleTransactionValidationException validationException)
            when (validationException.InnerException is NotFoundSaleTransactionException)
            {
                return NotFound(validationException.InnerException);
            }
            catch (SaleTransactionValidationException validationException)
            {
                return BadRequest(validationException.InnerException);
            }
            catch (SaleTransactionDependencyValidationException SaleTransactionDependencyValidationException)
            when (SaleTransactionDependencyValidationException.InnerException is AlreadyExistsSaleTransactionException)
            {
                return Conflict(SaleTransactionDependencyValidationException.InnerException);
            }
            catch (SaleTransactionDependencyValidationException dependencyValidationException)
            {
                return BadRequest(dependencyValidationException.InnerException);
            }
            catch (SaleTransactionDependencyException dependencyException)
            {
                return InternalServerError(dependencyException.InnerException);
            }
            catch (SaleTransactionServiceException serviceException)
            {
                return InternalServerError(serviceException.InnerException);
            }
        }

        [HttpDelete("{saleTransactionId}")]
        public async ValueTask<ActionResult<SaleTransaction>> DeleteSaleTransactionByIdAsync(Guid saleTransactionId)
        {
            try
            {
                SaleTransaction deletedSaleTransaction =
                    await this.saleTransactionService.RemoveSaleTransactionByIdAsync(saleTransactionId);
                return Ok(deletedSaleTransaction);
            }
            catch (SaleTransactionValidationException saleTransactionValidationException)
            when (saleTransactionValidationException.InnerException is NotFoundSaleTransactionException)
            {
                return NotFound(saleTransactionValidationException.InnerException);
            }
            catch (SaleTransactionValidationException validationException)
            {
                return BadRequest(validationException.InnerException);
            }
            catch (SaleTransactionDependencyValidationException SaleTransactionDependencyValidationException)
            when (SaleTransactionDependencyValidationException.InnerException is NotFoundSaleTransactionException)
            {
                return Conflict(SaleTransactionDependencyValidationException.InnerException);
            }
            catch (SaleTransactionDependencyValidationException dependencyValidationException)
            {
                return BadRequest(dependencyValidationException.InnerException);
            }
            catch (SaleTransactionDependencyException dependencyException)
            {
                return InternalServerError(dependencyException.InnerException);
            }
            catch (SaleTransactionServiceException serviceException)
            {
                return InternalServerError(serviceException.InnerException);
            }
        }
    }
}
