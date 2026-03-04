//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Models.Foundations.PropertySales;
using Sheenam.Blazor.Models.Foundations.SaleTransactions;
using Sheenam.Blazor.Services.Foundations.Guests;
using Sheenam.Blazor.Services.Foundations.Hosts;
using Sheenam.Blazor.Services.Foundations.PropertySales;
using Sheenam.Blazor.Services.Foundations.SaleTransactions;
using Host = Sheenam.Blazor.Models.Foundations.Hosts.Host;
namespace Sheenam.Blazor.Views.Components
{
    public partial class SaleTransactionFormComponent : ComponentBase
    {
        [Parameter]
        public EventCallback OnSaleTransactionAdded { get; set; }

        [Inject]
        public ISaleTransactionService SaleTransactionService { get; set; }

        [Inject]
        public IPropertySaleService PropertySaleService { get; set; }

        [Inject]
        public IHostService HostService { get; set; }

        [Inject]
        public IGuestService GuestService { get; set; }

        public SaleTransaction saleTransaction = new SaleTransaction();
        public string errorMessage { get; set; } = string.Empty;
        public IEnumerable<PropertySale> PropertySales { get; set; } = new List<PropertySale>();
        public IEnumerable<Host> Hosts { get; set; } = new List<Host>();
        public IEnumerable<Guest> Guests { get; set; } = new List<Guest>();
        public DateTime transactionDate { get; set; } = DateTime.Today;

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
                this.Hosts = await this.HostService.RetrieveAllHostsAsync();
                this.Guests = await this.GuestService.RetrieveAllGuestsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dropdowns: {ex.Message}");
            }
        }

        public void EditSaleTransaction(SaleTransaction saleTransactionToEdit)
        {
            this.saleTransaction = new SaleTransaction
            {
                Id = saleTransactionToEdit.Id,
                PropertySaleId = saleTransactionToEdit.PropertySaleId,
                SellerId = saleTransactionToEdit.SellerId,
                BuyerId = saleTransactionToEdit.BuyerId,
                FinalPrice = saleTransactionToEdit.FinalPrice,
                TransactionDate = saleTransactionToEdit.TransactionDate,
                ContractDocument = saleTransactionToEdit.ContractDocument,
                Status = saleTransactionToEdit.Status,
                CreatedDate = saleTransactionToEdit.CreatedDate,
                UpdatedDate = saleTransactionToEdit.UpdatedDate
            };

            this.transactionDate = saleTransactionToEdit.TransactionDate.DateTime;
            StateHasChanged();
        }

        private void ResetForm()
        {
            this.saleTransaction = new SaleTransaction { Id = Guid.Empty };
            this.transactionDate = DateTime.Today;
            this.errorMessage = string.Empty;
        }

        private async Task HandleSubmit()
        {
            if (saleTransaction.PropertySaleId == Guid.Empty)
            {
                errorMessage = "Iltimos, mulkni tanlang!";
                return;
            }

            if (saleTransaction.SellerId == Guid.Empty)
            {
                errorMessage = "Iltimos, sotuvchini tanlang!";
                return;
            }

            if (saleTransaction.BuyerId == Guid.Empty)
            {
                errorMessage = "Iltimos, xaridorni tanlang!";
                return;
            }

            if (saleTransaction.FinalPrice <= 0)
            {
                errorMessage = "Yakuniy narx 0 dan katta bo'lishi kerak!";
                return;
            }

            try
            {
                errorMessage = string.Empty;
                saleTransaction.TransactionDate = new DateTimeOffset(transactionDate);

                if (this.saleTransaction.Id == Guid.Empty)
                {
                    this.saleTransaction.Id = Guid.NewGuid();
                    var now = DateTimeOffset.UtcNow;
                    this.saleTransaction.CreatedDate = now;
                    this.saleTransaction.UpdatedDate = now;
                    await this.SaleTransactionService.AddSaleTransactionAsync(this.saleTransaction);
                }
                else
                {
                    this.saleTransaction.UpdatedDate = DateTimeOffset.UtcNow;
                    await this.SaleTransactionService.ModifySaleTransactionAsync(this.saleTransaction);
                }

                await OnSaleTransactionAdded.InvokeAsync();
                ResetForm();
            }
            catch (Exception ex)
            {
                errorMessage = "Xatolik yuz berdi: " + ex.Message;
            }
        }
    }
}