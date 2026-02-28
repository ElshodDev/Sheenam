//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Models.Foundations.HomeRequests;
using Sheenam.Blazor.Services.Foundations.HomeRequests;

namespace Sheenam.Blazor.Views.Components
{
    public partial class HomeRequestFormComponent : ComponentBase
    {
        [Parameter]
        public EventCallback OnHomeRequestAdded { get; set; }

        [Inject]
        public IHomeRequestService HomeRequestService { get; set; }

        public HomeRequest homeRequest = new HomeRequest();
        public string guestIdString { get; set; } = string.Empty;
        public string homeIdString { get; set; } = string.Empty;
        public DateTime startDate { get; set; } = DateTime.Today;
        public DateTime endDate { get; set; } = DateTime.Today.AddDays(1);

        protected override void OnInitialized()
        {
            ResetForm();
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

            this.guestIdString = homeRequestToEdit.GuestId.ToString();
            this.homeIdString = homeRequestToEdit.HomeId.ToString();
            this.startDate = homeRequestToEdit.StartDate.DateTime;
            this.endDate = homeRequestToEdit.EndDate.DateTime;
            StateHasChanged();
        }

        private void ResetForm()
        {
            this.homeRequest = new HomeRequest { Id = Guid.Empty };
            this.guestIdString = string.Empty;
            this.homeIdString = string.Empty;
            this.startDate = DateTime.Today;
            this.endDate = DateTime.Today.AddDays(1);
        }

        private async Task HandleSubmit()
        {
            try
            {
                if (Guid.TryParse(guestIdString, out Guid guestId))
                    homeRequest.GuestId = guestId;

                if (Guid.TryParse(homeIdString, out Guid homeId))
                    homeRequest.HomeId = homeId;

                homeRequest.StartDate = new DateTimeOffset(startDate);
                homeRequest.EndDate = new DateTimeOffset(endDate);

                if (this.homeRequest.Id == Guid.Empty)
                {
                    this.homeRequest.Id = Guid.NewGuid();
                    await this.HomeRequestService.AddHomeRequestAsync(this.homeRequest);
                }
                else
                {
                    await this.HomeRequestService.ModifyHomeRequestAsync(this.homeRequest);
                }

                await OnHomeRequestAdded.InvokeAsync();
                ResetForm();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}