//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sheenam.Blazor.Models.Foundations.HomeRequests;
using Sheenam.Blazor.Services.Foundations.HomeRequests;

namespace Sheenam.Blazor.Views.Components
{
    public partial class HomeRequestComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<HomeRequest> OnEditSelected { get; set; }

        [Inject]
        public IHomeRequestService HomeRequestService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public IEnumerable<HomeRequest> HomeRequests { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadHomeRequestsAsync();
        }

        public async Task RefreshAsync()
        {
            await LoadHomeRequestsAsync();
            this.StateHasChanged();
        }

        private async Task LoadHomeRequestsAsync()
        {
            try
            {
                this.HomeRequests = await this.HomeRequestService.RetrieveAllHomeRequestsAsync();
            }
            catch (Exception)
            {
                this.ErrorMessage = "Ma'lumotlarni yuklashda xatolik yuz berdi.";
            }
        }

        private async Task DeleteHomeRequest(Guid homeRequestId)
        {
            bool confirmed = false;

            try
            {
                confirmed = await this.JSRuntime.InvokeAsync<bool>(
                    "confirm",
                    "Ushbu uy so'rovini o'chirmoqchimisiz?");
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
                    await this.HomeRequestService.RemoveHomeRequestByIdAsync(homeRequestId);
                    await RefreshAsync();
                }
                catch (Exception)
                {
                    this.ErrorMessage = "O'chirishda xatolik yuz berdi.";
                }
            }
        }

        private static string GetStatusBadge(HomeRequestStatus status) => status switch
        {
            HomeRequestStatus.Pending => "bg-warning text-dark",
            HomeRequestStatus.Approved => "bg-success",
            HomeRequestStatus.Rejected => "bg-danger",
            HomeRequestStatus.Cancelled => "bg-secondary",
            _ => "bg-light text-dark"
        };
    }
}