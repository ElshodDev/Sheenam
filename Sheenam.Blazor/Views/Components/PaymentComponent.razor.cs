//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sheenam.Blazor.Models.Foundations.Payments;
using Sheenam.Blazor.Services.Foundations.Payments;

namespace Sheenam.Blazor.Views.Components
{
    public partial class PaymentComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<Payment> OnEditSelected { get; set; }

        [Inject]
        public IPaymentService PaymentService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public IEnumerable<Payment> Payments { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadPaymentsAsync();
        }

        public async Task RefreshAsync()
        {
            await LoadPaymentsAsync();
            this.StateHasChanged();
        }

        private async Task LoadPaymentsAsync()
        {
            try
            {
                this.Payments = await this.PaymentService.RetrieveAllPaymentsAsync();
            }
            catch (Exception)
            {
                this.ErrorMessage = "Ma'lumotlarni yuklashda xatolik yuz berdi.";
            }
        }

        private async Task DeletePayment(Guid paymentId)
        {
            bool confirmed = false;

            try
            {
                confirmed = await this.JSRuntime.InvokeAsync<bool>(
                    "confirm",
                    "Ushbu to'lovni o'chirmoqchimisiz?");
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
                    await this.PaymentService.RemovePaymentByIdAsync(paymentId);
                    await RefreshAsync();
                }
                catch (Exception)
                {
                    this.ErrorMessage = "O'chirishda xatolik yuz berdi.";
                }
            }
        }

        private static string GetStatusBadge(PaymentStatus status) => status switch
        {
            PaymentStatus.Pending => "bg-warning text-dark",
            PaymentStatus.Completed => "bg-success",
            PaymentStatus.Failed => "bg-danger",
            PaymentStatus.Refunded => "bg-secondary",
            _ => "bg-light text-dark"
        };
    }
}