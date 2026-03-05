//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.Notifications;
namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string NotificationsRelativeUrl = "api/notifications";

        public async ValueTask<Notification> PostNotificationAsync(Notification notification) =>
            await PostAsync(NotificationsRelativeUrl, notification);

        public async ValueTask<List<Notification>> GetAllNotificationsAsync() =>
            await GetAsync<List<Notification>>(NotificationsRelativeUrl);

        public async ValueTask<Notification> GetNotificationByIdAsync(Guid notificationId) =>
            await GetAsync<Notification>($"{NotificationsRelativeUrl}/{notificationId}");

        public async ValueTask<Notification> PutNotificationAsync(Notification notification) =>
            await PutAsync($"{NotificationsRelativeUrl}/{notification.Id}", notification);

        public async ValueTask<Notification> DeleteNotificationByIdAsync(Guid notificationId) =>
            await DeleteAsync<Notification>($"{NotificationsRelativeUrl}/{notificationId}");
    }
}