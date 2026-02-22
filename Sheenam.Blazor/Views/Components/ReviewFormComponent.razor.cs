//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Models.Foundations.Reviews;
using Sheenam.Blazor.Services.Foundations.Reviews;

namespace Sheenam.Blazor.Views.Components
{
    public partial class ReviewFormComponent : ComponentBase
    {
        private Review review = new();
        private string isPositiveText;

        public string ErrorMessage { get; set; }

        [Inject]
        public IReviewService ReviewService { get; set; }

        [Parameter]
        public EventCallback OnReviewAdded { get; set; }

        public void EditReview(Review selectedReview)
        {
            this.review = new Review
            {
                Id = selectedReview.Id,
                UserId = selectedReview.UserId,
                HomeId = selectedReview.HomeId,
                PropertySaleId = selectedReview.PropertySaleId,
                IsPositive = selectedReview.IsPositive,
                Rating = selectedReview.Rating,
                Comment = selectedReview.Comment,
                CreatedDate = selectedReview.CreatedDate,
                UpdatedDate = selectedReview.UpdatedDate
            };

            this.isPositiveText = selectedReview.IsPositive?.ToString()?.ToLowerInvariant();
            this.ErrorMessage = null;
            this.StateHasChanged();
        }

        private async Task HandleSubmit()
        {
            try
            {
                this.ErrorMessage = null;

                this.review.UserId = this.review.UserId == Guid.Empty
                    ? Guid.NewGuid()
                    : this.review.UserId;

                this.review.IsPositive =
                    bool.TryParse(this.isPositiveText, out bool parsed)
                        ? parsed
                        : CalculateIsPositiveFromRating(this.review.Rating);

                if (this.review.Id == Guid.Empty)
                {
                    await this.ReviewService.AddReviewAsync(this.review);
                }
                else
                {
                    await this.ReviewService.ModifyReviewAsync(this.review);
                }

                ResetForm();
                await this.OnReviewAdded.InvokeAsync();
            }
            catch (Exception)
            {
                this.ErrorMessage = "Saqlashda xatolik yuz berdi.";
            }
        }

        private static bool? CalculateIsPositiveFromRating(int rating)
        {
            if (rating >= 4) return true;
            if (rating <= 2) return false;
            return null;
        }

        private void ResetForm()
        {
            this.review = new Review();
            this.isPositiveText = null;
            this.ErrorMessage = null;
            this.StateHasChanged();
        }
    }
}
