//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Models.Foundations.PropertySales;
using Sheenam.Blazor.Models.Foundations.SaleOffers;
using Sheenam.Blazor.Services.Foundations.Guests;
using Sheenam.Blazor.Services.Foundations.PropertySales;
using Sheenam.Blazor.Services.Foundations.SaleOffers;
namespace Sheenam.Blazor.Views.Components
{
    public partial class SaleOfferFormComponent : ComponentBase
    {
        [Parameter]
        public EventCallback OnSaleOfferAdded { get; set; }

        [Inject]
        public ISaleOfferService SaleOfferService { get; set; }

        [Inject]
        public IPropertySaleService PropertySaleService { get; set; }

        [Inject]
        public IGuestService GuestService { get; set; }

        public SaleOffer saleOffer = new SaleOffer();
        public string errorMessage { get; set; } = string.Empty;
        public IEnumerable<PropertySale> PropertySales { get; set; } = new List<PropertySale>();
        public IEnumerable<Guest> Guests { get; set; } = new List<Guest>();

        protected override async Task OnInitializedAsync()
        {
            await LoadDropdownsAsync();
            ResetForm();
        }

        private async Task LoadDropdownsAsync()
        {
            try
            {
                this.PropertySales = await this.PropertySaleService.RetrieveAllPropertySalesAsync();
                this.Guests = await this.GuestService.RetrieveAllGuestsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dropdowns: {ex.Message}");
            }
        }

        public void EditSaleOffer(SaleOffer saleOfferToEdit)
        {
            this.saleOffer = new SaleOffer
            {
                Id = saleOfferToEdit.Id,
                PropertySaleId = saleOfferToEdit.PropertySaleId,
                BuyerId = saleOfferToEdit.BuyerId,
                OfferPrice = saleOfferToEdit.OfferPrice,
                Message = saleOfferToEdit.Message,
                Status = saleOfferToEdit.Status,
                CreatedDate = saleOfferToEdit.CreatedDate,
                ResponseDate = saleOfferToEdit.ResponseDate,
                RejectionReason = saleOfferToEdit.RejectionReason
            };
            StateHasChanged();
        }

        private void ResetForm()
        {
            this.saleOffer = new SaleOffer { Id = Guid.Empty };
            this.errorMessage = string.Empty;
        }

        private async Task HandleSubmit()
        {
            if (saleOffer.PropertySaleId == Guid.Empty)
            {
                errorMessage = "Iltimos, mulkni tanlang!";
                return;
            }

            if (saleOffer.BuyerId == Guid.Empty)
            {
                errorMessage = "Iltimos, xaridorni tanlang!";
                return;
            }

            if (saleOffer.OfferPrice <= 0)
            {
                errorMessage = "Taklif narxi 0 dan katta bo'lishi kerak!";
                return;
            }

            try
            {
                errorMessage = string.Empty;

                if (this.saleOffer.Id == Guid.Empty)
                {
                    this.saleOffer.Id = Guid.NewGuid();
                    this.saleOffer.CreatedDate = DateTimeOffset.UtcNow;
                    await this.SaleOfferService.AddSaleOfferAsync(this.saleOffer);
                }
                else
                {
                    await this.SaleOfferService.ModifySaleOfferAsync(this.saleOffer);
                }

                await OnSaleOfferAdded.InvokeAsync();
                ResetForm();
            }
            catch (Exception ex)
            {
                errorMessage = "Xatolik yuz berdi: " + ex.Message;
            }
        }
    }
}