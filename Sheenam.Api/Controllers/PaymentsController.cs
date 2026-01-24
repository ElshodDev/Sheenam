//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Payments;
using Sheenam.Api.Models.Foundations.Payments.Exceptions;
using Sheenam.Api.Services.Foundations.Payments;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : RESTFulController
    {
        private readonly IPaymentService paymentService;

        public PaymentsController(IPaymentService paymentService) =>
              this.paymentService = paymentService;
        [HttpPost]
        //[Authorize]
        public async ValueTask<ActionResult<Payment>> PostPaymentAsync(Payment payment)
        {
            try
            {
                Payment postedPayment =
                    await this.paymentService.AddPaymentAsync(payment);

                return Created(postedPayment);
            }
            catch (PaymentValidationException paymentValidationException)
            {
                return BadRequest(paymentValidationException.InnerException);
            }
            catch (PaymentDependencyValidationException paymentDependencyValidationException)
               when (paymentDependencyValidationException.InnerException is AlreadyExistsPaymentException)
            {
                return Conflict(paymentDependencyValidationException.InnerException);
            }
            catch (PaymentDependencyValidationException paymentDependencyValidationException)
            {
                return BadRequest(paymentDependencyValidationException.InnerException);
            }
            catch (PaymentDependencyException paymentDependencyException)
            {
                return InternalServerError(paymentDependencyException.InnerException);
            }
            catch (PaymentServiceException paymentServiceException)
            {
                return InternalServerError(paymentServiceException.InnerException);
            }

        }

        [HttpGet]
      //  [Authorize(Roles = "Admin, Host")]
        public ActionResult<IQueryable<Payment>> GetAllPayments()
        {
            try
            {
                IQueryable<Payment> retrievedPayments =
                    this.paymentService.RetrieveAllPayments();
                return Ok(retrievedPayments);
            }
            catch (PaymentDependencyException paymentDependencyException)
            {
                return InternalServerError(paymentDependencyException.InnerException);
            }
            catch (PaymentServiceException paymentServiceException)
            {
                return InternalServerError(paymentServiceException.InnerException);
            }

        }

        [HttpGet("{paymentId}")]
       // [Authorize]
        public async ValueTask<ActionResult<Payment>> GetPaymentByIdAsync(Guid paymentId)
        {
            try
            {
                Payment retrievedPayment =
                    await this.paymentService.RetrievePaymentByIdAsync(paymentId);
                return Ok(retrievedPayment);
            }
            catch (PaymentValidationException paymentValidationException)
             when (paymentValidationException.InnerException is NotFoundPaymentException)
            {
                return NotFound(paymentValidationException.InnerException);
            }
            catch (PaymentValidationException paymentValidationException)
            {
                return BadRequest(paymentValidationException.InnerException);
            }
            catch (PaymentDependencyException paymentDependencyException)
            {
                return InternalServerError(paymentDependencyException.InnerException);
            }
            catch (PaymentServiceException paymentServiceException)
            {
                return InternalServerError(paymentServiceException.InnerException);
            }
        }

        [HttpPut("{paymentId}")]
       // [Authorize(Roles = "Admin, Host")]
        public async ValueTask<ActionResult<Payment>> PutPaymentAsync(Guid paymentId, Payment payment)
        {
            try
            {
                payment.Id = paymentId;
                Payment modifiedPayment =
                    await this.paymentService.ModifyPaymentAsync(payment);
                return Ok(modifiedPayment);
            }
            catch (PaymentValidationException paymentValidationException)
             when (paymentValidationException.InnerException is NotFoundPaymentException)
            {
                return NotFound(paymentValidationException.InnerException);
            }
            catch (PaymentValidationException paymentValidationException)
            {
                return BadRequest(paymentValidationException.InnerException);
            }
            catch (PaymentDependencyValidationException paymentDependencyValidationException)
               when (paymentDependencyValidationException.InnerException is NotFoundPaymentException)
            {
                return NotFound(paymentDependencyValidationException.InnerException);
            }
            catch (PaymentDependencyValidationException paymentDependencyValidationException)
            {
                return BadRequest(paymentDependencyValidationException.InnerException);
            }
            catch (PaymentDependencyException paymentDependencyException)
            {
                return InternalServerError(paymentDependencyException.InnerException);
            }
            catch (PaymentServiceException paymentServiceException)
            {
                return InternalServerError(paymentServiceException.InnerException);
            }

        }

        [HttpDelete("{paymentId}")]
       // [Authorize(Roles = "Admin")]
        public async ValueTask<ActionResult<Payment>> DeletePaymentByIdAsync(Guid paymentId)
        {
            try
            {
                Payment deletedPayment =
                    await this.paymentService.RemovePaymentByIdAsync(paymentId);
                return Ok(deletedPayment);
            }
            catch (PaymentValidationException paymentValidationException)
             when (paymentValidationException.InnerException is NotFoundPaymentException)
            {
                return NotFound(paymentValidationException.InnerException);
            }
            catch (PaymentValidationException paymentValidationException)
            {
                return BadRequest(paymentValidationException.InnerException);
            }
            catch (PaymentDependencyValidationException paymentDependencyValidationException)
               when (paymentDependencyValidationException.InnerException is NotFoundPaymentException)
            {
                return NotFound(paymentDependencyValidationException.InnerException);
            }
            catch (PaymentDependencyValidationException paymentDependencyValidationException)
            {
                return BadRequest(paymentDependencyValidationException.InnerException);
            }
            catch (PaymentDependencyException paymentDependencyException)
            {
                return InternalServerError(paymentDependencyException.InnerException);
            }
            catch (PaymentServiceException paymentServiceException)
            {
                return InternalServerError(paymentServiceException.InnerException);
            }
        }
    }
}
