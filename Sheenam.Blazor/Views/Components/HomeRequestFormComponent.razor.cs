//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Models.Foundations.HomeRequests;
using Sheenam.Blazor.Models.Foundations.Homes;
using Sheenam.Blazor.Services.Foundations.Guests;
using Sheenam.Blazor.Services.Foundations.HomeRequests;
using Sheenam.Blazor.Services.Foundations.Homes;

namespace Sheenam.Blazor.Views.Components
{
    public partial class HomeRequestFormComponent : ComponentBase
    {
        [Parameter]
        public EventCallback OnHomeRequestAdded { get; set; }

        [Inject]
        public IHomeRequestService HomeRequestService { get; set; }

        [Inject]
        public IGuestService GuestService { get; set; }

        [Inject]
        public IHomeService HomeService { get; set; }

        public HomeRequest homeRequest = new HomeRequest();
        public string errorMessage { get; set; } = string.Empty;
        public IEnumerable<Guest> Guests { get; set; } = new List<Guest>();
        public IEnumerable<Home> Homes { get; set; } = new List<Home>();
        public DateTime startDate { get; set; } = DateTime.Today;
        public DateTime endDate { get; set; } = DateTime.Today.AddDays(1);

        protected override async Task OnInitializedAsync()
        {
            await LoadDropdownsAsync();
            ResetForm();
        }

        private async Task LoadDropdownsAsync()
        {
            try
            {
                this.Guests = await this.GuestService.RetrieveAllGuestsAsync();
                this.Homes = await this.HomeService.RetrieveAllHomesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dropdowns: {ex.Message}");
            }
        }

        public void EditHomeRequest(HomeRequest homeRequestToEdit)
        {
            this.homeRequest = new HomeRequest
            {
                Id = homeRequestToEdit.Id,
                GuestId = homeRequestToEdit.GuestId,
                HomeId = homeRequestToEdit.HomeId,
                Message = homeRequestToEdit.Message,
                StartDate = homeRequestToEdit.StartDate,
                EndDate = homeRequestToEdit.EndDate,
                Status = homeRequestToEdit.Status,
                RejectionReason = homeRequestToEdit.RejectionReason
            };

            this.startDate = homeRequestToEdit.StartDate.DateTime;
            this.endDate = homeRequestToEdit.EndDate.DateTime;
            StateHasChanged();
        }

        private void ResetForm()
        {
            this.homeRequest = new HomeRequest { Id = Guid.Empty };
            this.startDate = DateTime.Today;
            this.endDate = DateTime.Today.AddDays(1);
        }

        private async Task HandleSubmit()
        {
            if (homeRequest.GuestId == Guid.Empty)
            {
                errorMessage = "Iltimos, mehmonni tanlang!";
                return;
            }

            if (homeRequest.HomeId == Guid.Empty)
            {
                errorMessage = "Iltimos, uyni tanlang!";
                return;
            }

            try
            {
                errorMessage = string.Empty;
                homeRequest.StartDate = new DateTimeOffset(startDate);
                homeRequest.EndDate = new DateTimeOffset(endDate);

                if (this.homeRequest.Id == Guid.Empty)
                {
                    this.homeRequest.Id = Guid.NewGuid();
                    this.homeRequest.CreatedDate = DateTimeOffset.UtcNow;
                    this.homeRequest.UpdatedDate = DateTimeOffset.UtcNow;
                    await this.HomeRequestService.AddHomeRequestAsync(this.homeRequest);
                }
                else
                {
                    this.homeRequest.CreatedDate = DateTimeOffset.UtcNow;
                    this.homeRequest.UpdatedDate = DateTimeOffset.UtcNow;
                    await this.HomeRequestService.ModifyHomeRequestAsync(this.homeRequest);
                }

                await OnHomeRequestAdded.InvokeAsync();
                ResetForm();
            }
            catch (Exception ex)
            {
                errorMessage = "Xatolik yuz berdi: " + ex.Message;
            }
        }
    }
}