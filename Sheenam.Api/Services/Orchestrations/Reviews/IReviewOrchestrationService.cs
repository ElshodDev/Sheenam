//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Reviews;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Orchestrations.Reviews
{
    public interface IReviewOrchestrationService
    {
        ValueTask<Review> SubmitReviewAsync(Review review);
    }
}
