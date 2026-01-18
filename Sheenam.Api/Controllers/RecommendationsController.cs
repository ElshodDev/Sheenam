//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.AI.Recommendations;
using Sheenam.Api.Services.AI.Recommendations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationsController : RESTFulController
    {
        private readonly IRecommendationService recommendationService;

        public RecommendationsController(IRecommendationService recommendationService)
        {
            this.recommendationService = recommendationService;
        }

        /// <summary>
        /// Get AI-powered home recommendations based on preferences
        /// </summary>
        [HttpPost("homes")]
        [AllowAnonymous]
        public async ValueTask<ActionResult<List<HomeRecommendation>>> GetRecommendedHomes(
            [FromBody] GuestPreferences preferences,
            [FromQuery] int topN = 10)
        {
            try
            {
                var recommendations = await this.recommendationService
                    .GetRecommendedHomesAsync(preferences, topN);

                return Ok(recommendations);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        /// <summary>
        /// Train/Retrain the recommendation ML model (Admin only)
        /// </summary>
        [HttpPost("train")]
        [Authorize(Roles = "Admin")]
        public async ValueTask<ActionResult> TrainModel()
        {
            try
            {
                await this.recommendationService.TrainModelAsync();

                return Ok(new
                {
                    message = "Model trained successfully",
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
    }
}
