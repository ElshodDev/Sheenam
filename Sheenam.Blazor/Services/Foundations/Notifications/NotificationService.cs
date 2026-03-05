//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.Notifications;
namespace Sheenam.Blazor.Services.Foundations.Notifications
{
    public partial class NotificationService : INotificationService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public NotificationService(IApiBroker apiBroker, ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Notification> AddNotificationAsync(Notification notification) =>
            await TryCatch(async () =>
            {
                ValidateNotificationOnAdd(notification);
                return await this.apiBroker.PostNotificationAsync(notification);
            });

        public async ValueTask<IQueryable<Notification>> RetrieveAllNotificationsAsync() =>
            await TryCatch(async () =>
            {
                var notifications = await this.apiBroker.GetAllNotificationsAsync();
                return notifications.AsQueryable();
            });

        public async ValueTask<Notification> RetrieveNotificationByIdAsync(Guid notificationId) =>
            await TryCatch(async () =>
            {
                ValidateNotificationId(notificationId);
                return await this.apiBroker.GetNotificationByIdAsync(notificationId);
            });

        public async ValueTask<Notification> ModifyNotificationAsync(Notification notification) =>
            await TryCatch(async () =>
            {
                ValidateNotificationOnModify(notification);
                return await this.apiBroker.PutNotificationAsync(notification);
            });

        public async ValueTask<Notification> RemoveNotificationByIdAsync(Guid notificationId) =>
            await TryCatch(async () =>
            {
                ValidateNotificationId(notificationId);
                return await this.apiBroker.DeleteNotificationByIdAsync(notificationId);
            });
    }
}