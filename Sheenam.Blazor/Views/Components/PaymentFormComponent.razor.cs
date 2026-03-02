//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Models.Foundations.Payments;
using Sheenam.Blazor.Models.Foundations.RentalContracts;
using Sheenam.Blazor.Models.Foundations.Users;
using Sheenam.Blazor.Services.Foundations.Payments;
using Sheenam.Blazor.Services.Foundations.RentalContracts;
using Sheenam.Blazor.Services.Foundations.Users;

namespace Sheenam.Blazor.Views.Components
{
    public partial class PaymentFormComponent : ComponentBase
    {
        [Parameter]
        public EventCallback OnPaymentAdded { get; set; }

        [Inject]
        public IPaymentService PaymentService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public IRentalContractService RentalContractService { get; set; }

        public Payment payment = new Payment();
        public string errorMessage { get; set; } = string.Empty;
        public IEnumerable<User> Users { get; set; } = new List<User>();
        public IEnumerable<RentalContract> RentalContracts { get; set; } = new List<RentalContract>();
        public DateTime paymentDate { get; set; } = DateTime.Today;

        protected override async Task OnInitializedAsync()
        {
            await LoadDropdownsAsync();
            ResetForm();
        }

        private async Task LoadDropdownsAsync()
        {
            try
            {
                this.Users = await this.UserService.RetrieveAllUsersAsync();
                this.RentalContracts = await this.RentalContractService.RetrieveAllRentalContractsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dropdowns: {ex.Message}");
            }
        }

        public void EditPayment(Payment paymentToEdit)
        {
            this.payment = new Payment
            {
                Id = paymentToEdit.Id,
                UserId = paymentToEdit.UserId,
                RentalContractId = paymentToEdit.RentalContractId,
                SaleTransactionId = paymentToEdit.SaleTransactionId,
                Amount = paymentToEdit.Amount,
                Method = paymentToEdit.Method,
                Status = paymentToEdit.Status,
                TransactionReference = paymentToEdit.TransactionReference,
                PaymentDate = paymentToEdit.PaymentDate,
                CreatedDate = paymentToEdit.CreatedDate,
                UpdatedDate = paymentToEdit.UpdatedDate
            };

            this.paymentDate = paymentToEdit.PaymentDate.DateTime;
            StateHasChanged();
        }

        private void ResetForm()
        {
            this.payment = new Payment { Id = Guid.Empty };
            this.paymentDate = DateTime.Today;
            this.errorMessage = string.Empty;
        }

        private async Task HandleSubmit()
        {
            if (payment.UserId == Guid.Empty)
            {
                errorMessage = "Iltimos, foydalanuvchini tanlang!";
                return;
            }

            if (payment.Amount <= 0)
            {
                errorMessage = "Miqdor 0 dan katta bo'lishi kerak!";
                return;
            }

            if (string.IsNullOrWhiteSpace(payment.TransactionReference))
            {
                errorMessage = "Iltimos, tranzaksiya raqamini kiriting!";
                return;
            }

            try
            {
                errorMessage = string.Empty;
                payment.PaymentDate = new DateTimeOffset(paymentDate);

                if (this.payment.Id == Guid.Empty)
                {
                    this.payment.Id = Guid.NewGuid();
                    var now = DateTimeOffset.UtcNow;
                    this.payment.CreatedDate = now;
                    this.payment.UpdatedDate = now;
                    await this.PaymentService.AddPaymentAsync(this.payment);
                }
                else
                {
                    this.payment.UpdatedDate = DateTimeOffset.UtcNow;
                    await this.PaymentService.ModifyPaymentAsync(this.payment);
                }

                await OnPaymentAdded.InvokeAsync();
                ResetForm();
            }
            catch (Exception ex)
            {
                errorMessage = "Xatolik yuz berdi: " + ex.Message;
            }
        }
    }
}