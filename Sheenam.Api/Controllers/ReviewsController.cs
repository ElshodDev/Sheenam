//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Reviews;
using Sheenam.Api.Models.Foundations.Reviews.Exceptions;
using Sheenam.Api.Services.Foundations.Reviews;
using Sheenam.Api.Services.Orchestrations.Reviews;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : RESTFulController
    {
        private readonly IReviewOrchestrationService reviewOrchestrationService;
        private readonly IReviewService reviewService;

        public ReviewsController(
            IReviewOrchestrationService reviewOrchestrationService,
            IReviewService reviewService)
        {
            this.reviewOrchestrationService = reviewOrchestrationService;
            this.reviewService = reviewService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Review>> PostReviewAsync(Review review)
        {
            try
            {
                review.Id = Guid.NewGuid();
                review.CreatedDate = DateTimeOffset.UtcNow;
                review.UpdatedDate = DateTimeOffset.UtcNow;

                Review addedReview =
                    await this.reviewOrchestrationService.SubmitReviewAsync(review);

                return Created(addedReview);
            }
            catch (ReviewValidationException reviewValidationException)
            {
                return BadRequest(reviewValidationException.InnerException);
            }
            catch (ReviewDependencyValidationException reviewDependencyValidationException)
                when (reviewDependencyValidationException.InnerException is AlreadyExistsReviewException)
            {
                return Conflict(reviewDependencyValidationException.InnerException);
            }
            catch (ReviewDependencyValidationException reviewDependencyValidationException)
            {
                return BadRequest(reviewDependencyValidationException.InnerException);
            }
            catch (ReviewDependencyException reviewDependencyException)
            {
                return InternalServerError(reviewDependencyException);
            }
            catch (ReviewServiceException reviewServiceException)
            {
                return InternalServerError(reviewServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Review>> GetAllReviews()
        {
            try
            {
                IQueryable<Review> retrievedReviews =
                    this.reviewService.RetrieveAllReviews();

                return Ok(retrievedReviews);
            }
            catch (ReviewDependencyException reviewDependencyException)
            {
                return InternalServerError(reviewDependencyException);
            }
            catch (ReviewServiceException reviewServiceException)
            {
                return InternalServerError(reviewServiceException);
            }
        }

        [HttpGet("{reviewId}")]
        public async ValueTask<ActionResult<Review>> GetReviewByIdAsync(Guid reviewId)
        {
            try
            {
                Review retrievedReview =
                    await this.reviewService.RetrieveReviewByIdAsync(reviewId);

                return Ok(retrievedReview);
            }
            catch (ReviewValidationException reviewValidationException)
                when (reviewValidationException.InnerException is NotFoundReviewException)
            {
                return NotFound(reviewValidationException.InnerException);
            }
            catch (ReviewValidationException reviewValidationException)
            {
                return BadRequest(reviewValidationException.InnerException);
            }
            catch (ReviewDependencyException reviewDependencyException)
            {
                return InternalServerError(reviewDependencyException);
            }
            catch (ReviewServiceException reviewServiceException)
            {
                return InternalServerError(reviewServiceException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Review>> PutReviewAsync(Review review)
        {
            try
            {
                Review modifiedReview =
                    await this.reviewService.ModifyReviewAsync(review);

                return Ok(modifiedReview);
            }
            catch (ReviewValidationException reviewValidationException)
                when (reviewValidationException.InnerException is NotFoundReviewException)
            {
                return NotFound(reviewValidationException.InnerException);
            }
            catch (ReviewValidationException reviewValidationException)
            {
                return BadRequest(reviewValidationException.InnerException);
            }
            catch (ReviewDependencyValidationException reviewDependencyValidationException)
                when (reviewDependencyValidationException.InnerException is AlreadyExistsReviewException)
            {
                return Conflict(reviewDependencyValidationException.InnerException);
            }
            catch (ReviewDependencyValidationException reviewDependencyValidationException)
                when (reviewDependencyValidationException.InnerException is LockedReviewException)
            {
                return Locked(reviewDependencyValidationException.InnerException);
            }
            catch (ReviewDependencyValidationException reviewDependencyValidationException)
            {
                return BadRequest(reviewDependencyValidationException.InnerException);
            }
            catch (ReviewDependencyException reviewDependencyException)
            {
                return InternalServerError(reviewDependencyException);
            }
            catch (ReviewServiceException reviewServiceException)
            {
                return InternalServerError(reviewServiceException);
            }
        }

        [HttpDelete("{reviewId}")]
        public async ValueTask<ActionResult<Review>> DeleteReviewByIdAsync(Guid reviewId)
        {
            try
            {
                Review deletedReview =
                    await this.reviewService.RemoveReviewAsync(reviewId);

                return Ok(deletedReview);
            }
            catch (ReviewValidationException reviewValidationException)
                when (reviewValidationException.InnerException is NotFoundReviewException)
            {
                return NotFound(reviewValidationException.InnerException);
            }
            catch (ReviewValidationException reviewValidationException)
            {
                return BadRequest(reviewValidationException.InnerException);
            }
            catch (ReviewDependencyValidationException reviewDependencyValidationException)
                when (reviewDependencyValidationException.InnerException is LockedReviewException)
            {
                return Locked(reviewDependencyValidationException.InnerException);
            }
            catch (ReviewDependencyValidationException reviewDependencyValidationException)
            {
                return BadRequest(reviewDependencyValidationException.InnerException);
            }
            catch (ReviewDependencyException reviewDependencyException)
            {
                return InternalServerError(reviewDependencyException);
            }
            catch (ReviewServiceException reviewServiceException)
            {
                return InternalServerError(reviewServiceException);
            }
        }
    }
}