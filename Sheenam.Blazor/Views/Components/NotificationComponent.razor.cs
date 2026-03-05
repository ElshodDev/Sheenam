//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sheenam.Blazor.Models.Foundations.Notifications;
using Sheenam.Blazor.Services.Foundations.Notifications;
namespace Sheenam.Blazor.Views.Components
{
    public partial class NotificationComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<Notification> OnEditSelected { get; set; }

        [Inject]
        public INotificationService NotificationService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public IEnumerable<Notification> Notifications { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync() =>
            await LoadNotificationsAsync();

        public async Task RefreshAsync()
        {
            await LoadNotificationsAsync();
            StateHasChanged();
        }

        private async Task LoadNotificationsAsync()
        {
            try
            {
                this.Notifications = await this.NotificationService.RetrieveAllNotificationsAsync();
            }
            catch (Exception)
            {
                this.ErrorMessage = "Ma'lumotlarni yuklashda xatolik yuz berdi.";
            }
        }

        private async Task DeleteNotification(Guid notificationId)
        {
            bool confirmed = await this.JSRuntime.InvokeAsync<bool>(
                "confirm", "Ushbu bildirishnomani o'chirmoqchimisiz?");

            if (confirmed)
            {
                try
                {
                    await this.NotificationService.RemoveNotificationByIdAsync(notificationId);
                    await RefreshAsync();
                }
                catch (Exception)
                {
                    this.ErrorMessage = "O'chirishda xatolik yuz berdi.";
                }
            }
        }

        private static string GetTypeBadge(NotificationType type) => type switch
        {
            NotificationType.HomeRequestUpdate => "bg-info text-dark",
            NotificationType.PaymentReminder => "bg-warning text-dark",
            NotificationType.NewOffer => "bg-primary",
            NotificationType.ContractExpiring => "bg-danger",
            NotificationType.SaleOfferReceived => "bg-success",
            NotificationType.PropertySold => "bg-secondary",
            _ => "bg-light text-dark"
        };
    }
}