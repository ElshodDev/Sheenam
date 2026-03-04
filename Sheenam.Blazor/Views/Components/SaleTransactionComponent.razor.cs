//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sheenam.Blazor.Models.Foundations.SaleTransactions;
using Sheenam.Blazor.Services.Foundations.SaleTransactions;
namespace Sheenam.Blazor.Views.Components
{
    public partial class SaleTransactionComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<SaleTransaction> OnEditSelected { get; set; }

        [Inject]
        public ISaleTransactionService SaleTransactionService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public IEnumerable<SaleTransaction> SaleTransactions { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync() =>
            await LoadSaleTransactionsAsync();

        public async Task RefreshAsync()
        {
            await LoadSaleTransactionsAsync();
            StateHasChanged();
        }

        private async Task LoadSaleTransactionsAsync()
        {
            try
            {
                this.SaleTransactions = await this.SaleTransactionService.RetrieveAllSaleTransactionsAsync();
            }
            catch (Exception)
            {
                this.ErrorMessage = "Ma'lumotlarni yuklashda xatolik yuz berdi.";
            }
        }

        private async Task DeleteSaleTransaction(Guid saleTransactionId)
        {
            bool confirmed = await this.JSRuntime.InvokeAsync<bool>(
                "confirm", "Ushbu tranzaksiyani o'chirmoqchimisiz?");

            if (confirmed)
            {
                try
                {
                    await this.SaleTransactionService.RemoveSaleTransactionByIdAsync(saleTransactionId);
                    await RefreshAsync();
                }
                catch (Exception)
                {
                    this.ErrorMessage = "O'chirishda xatolik yuz berdi.";
                }
            }
        }

        private static string GetStatusBadge(TransactionStatus status) => status switch
        {
            TransactionStatus.Pending => "bg-warning text-dark",
            TransactionStatus.Completed => "bg-success",
            TransactionStatus.Cancelled => "bg-secondary",
            TransactionStatus.Failed => "bg-danger",
            _ => "bg-light text-dark"
        };
    }
}