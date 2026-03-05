//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Models.Foundations.Notifications;
using Sheenam.Blazor.Models.Foundations.Users;
using Sheenam.Blazor.Services.Foundations.Notifications;
using Sheenam.Blazor.Services.Foundations.Users;
namespace Sheenam.Blazor.Views.Components
{
    public partial class NotificationFormComponent : ComponentBase
    {
        [Parameter]
        public EventCallback OnNotificationAdded { get; set; }

        [Inject]
        public INotificationService NotificationService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        public Notification notification = new Notification();
        public string errorMessage { get; set; } = string.Empty;
        public IEnumerable<User> Users { get; set; } = new List<User>();

        protected override async Task OnInitializedAsync()
        {
            await LoadUsersAsync();
            ResetForm();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                this.Users = await this.UserService.RetrieveAllUsersAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading users: {ex.Message}");
            }
        }

        public void EditNotification(Notification notificationToEdit)
        {
            this.notification = new Notification
            {
                Id = notificationToEdit.Id,
                UserId = notificationToEdit.UserId,
                Title = notificationToEdit.Title,
                Message = notificationToEdit.Message,
                Type = notificationToEdit.Type,
                IsRead = notificationToEdit.IsRead,
                CreatedDate = notificationToEdit.CreatedDate
            };
            StateHasChanged();
        }

        private void ResetForm()
        {
            this.notification = new Notification { Id = Guid.Empty };
            this.errorMessage = string.Empty;
        }

        private async Task HandleSubmit()
        {
            if (notification.UserId == Guid.Empty)
            {
                errorMessage = "Iltimos, foydalanuvchini tanlang!";
                return;
            }

            if (string.IsNullOrWhiteSpace(notification.Title))
            {
                errorMessage = "Sarlavha kiritish majburiy!";
                return;
            }

            if (string.IsNullOrWhiteSpace(notification.Message))
            {
                errorMessage = "Xabar kiritish majburiy!";
                return;
            }

            try
            {
                errorMessage = string.Empty;

                if (this.notification.Id == Guid.Empty)
                {
                    this.notification.Id = Guid.NewGuid();
                    this.notification.CreatedDate = DateTimeOffset.UtcNow;
                    await this.NotificationService.AddNotificationAsync(this.notification);
                }
                else
                {
                    await this.NotificationService.ModifyNotificationAsync(this.notification);
                }

                await OnNotificationAdded.InvokeAsync();
                ResetForm();
            }
            catch (Exception ex)
            {
                errorMessage = "Xatolik yuz berdi: " + ex.Message;
            }
        }
    }
}