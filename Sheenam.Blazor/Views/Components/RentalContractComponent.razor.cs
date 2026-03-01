//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sheenam.Blazor.Models.Foundations.RentalContracts;
using Sheenam.Blazor.Services.Foundations.RentalContracts;

namespace Sheenam.Blazor.Views.Components
{
    public partial class RentalContractComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<RentalContract> OnEditSelected { get; set; }

        [Inject]
        public IRentalContractService RentalContractService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public IEnumerable<RentalContract> RentalContracts { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadRentalContractsAsync();
        }

        public async Task RefreshAsync()
        {
            await LoadRentalContractsAsync();
            this.StateHasChanged();
        }

        private async Task LoadRentalContractsAsync()
        {
            try
            {
                this.RentalContracts = await this.RentalContractService.RetrieveAllRentalContractsAsync();
            }
            catch (Exception)
            {
                this.ErrorMessage = "Ma'lumotlarni yuklashda xatolik yuz berdi.";
            }
        }

        private async Task DeleteRentalContract(Guid rentalContractId)
        {
            bool confirmed = false;

            try
            {
                confirmed = await this.JSRuntime.InvokeAsync<bool>(
                    "confirm",
                    "Ushbu ijara shartnomasini o'chirmoqchimisiz?");
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
                    await this.RentalContractService.RemoveRentalContractByIdAsync(rentalContractId);
                    await RefreshAsync();
                }
                catch (Exception)
                {
                    this.ErrorMessage = "O'chirishda xatolik yuz berdi.";
                }
            }
        }

        private static string GetStatusBadge(ContractStatus status) => status switch
        {
            ContractStatus.Active => "bg-success",
            ContractStatus.Expired => "bg-warning text-dark",
            ContractStatus.Terminated => "bg-danger",
            _ => "bg-light text-dark"
        };
    }
}