//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sheenam.Blazor.Models.Foundations.PropertySales;
using Sheenam.Blazor.Services.Foundations.PropertySales;
namespace Sheenam.Blazor.Views.Components
{
    public partial class PropertySaleComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<PropertySale> OnEditSelected { get; set; }

        [Inject]
        public IPropertySaleService PropertySaleService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public IEnumerable<PropertySale> PropertySales { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync() =>
            await LoadPropertySalesAsync();

        public async Task RefreshAsync()
        {
            await LoadPropertySalesAsync();
            StateHasChanged();
        }

        private async Task LoadPropertySalesAsync()
        {
            try
            {
                this.PropertySales = await this.PropertySaleService.RetrieveAllPropertySalesAsync();
            }
            catch (Exception)
            {
                this.ErrorMessage = "Ma'lumotlarni yuklashda xatolik yuz berdi.";
            }
        }

        private async Task DeletePropertySale(Guid propertySaleId)
        {
            bool confirmed = await this.JSRuntime.InvokeAsync<bool>(
                "confirm", "Ushbu mulkni o'chirmoqchimisiz?");

            if (confirmed)
            {
                try
                {
                    await this.PropertySaleService.RemovePropertySaleByIdAsync(propertySaleId);
                    await RefreshAsync();
                }
                catch (Exception)
                {
                    this.ErrorMessage = "O'chirishda xatolik yuz berdi.";
                }
            }
        }

        private static string GetStatusBadge(PropertySaleStatus status) => status switch
        {
            PropertySaleStatus.Available => "bg-success",
            PropertySaleStatus.Reserved => "bg-warning text-dark",
            PropertySaleStatus.Sold => "bg-secondary",
            PropertySaleStatus.Removed => "bg-danger",
            _ => "bg-light text-dark"
        };
    }
}