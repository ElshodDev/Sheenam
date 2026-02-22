//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sheenam.Blazor.Models.Foundations.Reviews;
using Sheenam.Blazor.Services.Foundations.Reviews;

namespace Sheenam.Blazor.Views.Components
{
    public partial class ReviewComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<Review> OnEditSelected { get; set; }

        [Inject]
        public IReviewService ReviewService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public IEnumerable<Review> Reviews { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync() =>
            await LoadReviewsAsync();

        public async Task RefreshAsync()
        {
            await LoadReviewsAsync();
            this.StateHasChanged();
        }

        private async Task LoadReviewsAsync()
        {
            try
            {
                this.Reviews = await this.ReviewService.RetrieveAllReviewsAsync();
            }
            catch (Exception)
            {
                this.ErrorMessage = "Ma'lumotlarni yuklashda xatolik yuz berdi.";
            }
        }

        private async Task DeleteReview(Guid reviewId)
        {
            bool confirmed;

            try
            {
                confirmed = await this.JSRuntime.InvokeAsync<bool>(
                    "confirm",
                    "Ushbu sharhni o'chirmoqchimisiz?");
            }
            catch (Exception)
            {
                this.ErrorMessage = "Tasdiqlash oynasini ko'rsatishda xatolik yuz berdi.";
                return;
            }

            if (confirmed)
            {
                try
                {
                    await this.ReviewService.RemoveReviewByIdAsync(reviewId);
                    await RefreshAsync();
                }
                catch (Exception)
                {
                    this.ErrorMessage = "O'chirishda xatolik yuz berdi.";
                }
            }
        }
    }
}
