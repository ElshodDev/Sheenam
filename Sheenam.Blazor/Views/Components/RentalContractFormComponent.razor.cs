//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Models.Foundations.HomeRequests;
using Sheenam.Blazor.Models.Foundations.Homes;
using Sheenam.Blazor.Models.Foundations.RentalContracts;
using Sheenam.Blazor.Services.Foundations.Guests;
using Sheenam.Blazor.Services.Foundations.HomeRequests;
using Sheenam.Blazor.Services.Foundations.Homes;
using Sheenam.Blazor.Services.Foundations.Hosts;
using Sheenam.Blazor.Services.Foundations.RentalContracts;
using Host = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Views.Components
{
    public partial class RentalContractFormComponent : ComponentBase
    {
        [Parameter]
        public EventCallback OnRentalContractAdded { get; set; }

        [Inject]
        public IRentalContractService RentalContractService { get; set; }

        [Inject]
        public IGuestService GuestService { get; set; }

        [Inject]
        public IHostService HostService { get; set; }

        [Inject]
        public IHomeService HomeService { get; set; }

        [Inject]
        public IHomeRequestService HomeRequestService { get; set; }

        public RentalContract rentalContract = new RentalContract();
        public IEnumerable<Guest> Guests { get; set; } = new List<Guest>();
        public IEnumerable<Host> Hosts { get; set; } = new List<Host>();
        public IEnumerable<Home> Homes { get; set; } = new List<Home>();
        public IEnumerable<HomeRequest> HomeRequests { get; set; } = new List<HomeRequest>();
        public DateTime startDate { get; set; } = DateTime.Today;
        public DateTime endDate { get; set; } = DateTime.Today.AddMonths(1);
        public DateTime signedDate { get; set; } = DateTime.Today;

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
                this.Hosts = await this.HostService.RetrieveAllHostsAsync();
                this.Homes = await this.HomeService.RetrieveAllHomesAsync();
                this.HomeRequests = await this.HomeRequestService.RetrieveAllHomeRequestsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dropdowns: {ex.Message}");
            }
        }

        public void EditRentalContract(RentalContract rentalContractToEdit)
        {
            this.rentalContract = new RentalContract
            {
                Id = rentalContractToEdit.Id,
                HomeRequestId = rentalContractToEdit.HomeRequestId,
                GuestId = rentalContractToEdit.GuestId,
                HostId = rentalContractToEdit.HostId,
                HomeId = rentalContractToEdit.HomeId,
                MonthlyRent = rentalContractToEdit.MonthlyRent,
                SecurityDeposit = rentalContractToEdit.SecurityDeposit,
                Terms = rentalContractToEdit.Terms,
                Status = rentalContractToEdit.Status,
                StartDate = rentalContractToEdit.StartDate,
                EndDate = rentalContractToEdit.EndDate,
                SignedDate = rentalContractToEdit.SignedDate
            };

            this.startDate = rentalContractToEdit.StartDate.DateTime;
            this.endDate = rentalContractToEdit.EndDate.DateTime;
            this.signedDate = rentalContractToEdit.SignedDate.DateTime;
            StateHasChanged();
        }

        private void ResetForm()
        {
            this.rentalContract = new RentalContract { Id = Guid.Empty };
            this.startDate = DateTime.Today;
            this.endDate = DateTime.Today.AddMonths(1);
            this.signedDate = DateTime.Today;
        }

        private async Task HandleSubmit()
        {
            try
            {
                rentalContract.StartDate = new DateTimeOffset(startDate);
                rentalContract.EndDate = new DateTimeOffset(endDate);
                rentalContract.SignedDate = new DateTimeOffset(signedDate);

                if (this.rentalContract.Id == Guid.Empty)
                {
                    this.rentalContract.Id = Guid.NewGuid();
                    this.rentalContract.CreatedDate = DateTimeOffset.UtcNow;
                    this.rentalContract.UpdatedDate = DateTimeOffset.UtcNow;
                    await this.RentalContractService.AddRentalContractAsync(this.rentalContract);
                }
                else
                {
                    this.rentalContract.UpdatedDate = DateTimeOffset.UtcNow;
                    await this.RentalContractService.ModifyRentalContractAsync(this.rentalContract);
                }

                await OnRentalContractAdded.InvokeAsync();
                ResetForm();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}