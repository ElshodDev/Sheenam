//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sheenam.Blazor.Models.Foundations.SaleOffers;
using Sheenam.Blazor.Services.Foundations.SaleOffers;
namespace Sheenam.Blazor.Views.Components
{
    public partial class SaleOfferComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<SaleOffer> OnEditSelected { get; set; }

        [Inject]
        public ISaleOfferService SaleOfferService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public IEnumerable<SaleOffer> SaleOffers { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync() =>
            await LoadSaleOffersAsync();

        public async Task RefreshAsync()
        {
            await LoadSaleOffersAsync();
            StateHasChanged();
        }

        private async Task LoadSaleOffersAsync()
        {
            try
            {
                this.SaleOffers = await this.SaleOfferService.RetrieveAllSaleOffersAsync();
            }
            catch (Exception)
            {
                this.ErrorMessage = "Ma'lumotlarni yuklashda xatolik yuz berdi.";
            }
        }

        private async Task DeleteSaleOffer(Guid saleOfferId)
        {
            bool confirmed = await this.JSRuntime.InvokeAsync<bool>(
                "confirm", "Ushbu taklifni o'chirmoqchimisiz?");

            if (confirmed)
            {
                try
                {
                    await this.SaleOfferService.RemoveSaleOfferByIdAsync(saleOfferId);
                    await RefreshAsync();
                }
                catch (Exception)
                {
                    this.ErrorMessage = "O'chirishda xatolik yuz berdi.";
                }
            }
        }

        private static string GetStatusBadge(SaleOfferStatus status) => status switch
        {
            SaleOfferStatus.Pending => "bg-warning text-dark",
            SaleOfferStatus.Accepted => "bg-success",
            SaleOfferStatus.Rejected => "bg-danger",
            SaleOfferStatus.Cancelled => "bg-secondary",
            _ => "bg-light text-dark"
        };
    }
}