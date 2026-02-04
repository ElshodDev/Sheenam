//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Notifications
{
    public partial class NotificationService : INotificationService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public NotificationService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Notification> AddNotificationAsync(Notification notification) =>
            await TryCatch(async () =>
            {
                ValidateNotificationOnAdd(notification);
                return await this.storageBroker.InsertNotificationAsync(notification);
            });

        public IQueryable<Notification> RetrieveAllNotifications() =>
            this.storageBroker.SelectAllNotifications();

        public async ValueTask<Notification> RetrieveNotificationByIdAsync(Guid notificationId) =>
            await TryCatch(async () =>
            {
                ValidateNotificationId(notificationId);
                Notification maybeNotification = await this.storageBroker.SelectNotificationByIdAsync(notificationId);
                ValidateStorageNotification(maybeNotification, notificationId);
                return maybeNotification;
            });

        public async ValueTask<Notification> ModifyNotificationAsync(Notification notification) =>
            await TryCatch(async () =>
            {
                ValidateNotificationOnModify(notification);
                Notification maybeNotification = await this.storageBroker.SelectNotificationByIdAsync(notification.Id);
                ValidateStorageNotification(maybeNotification, notification.Id);
                return await this.storageBroker.UpdateNotificationAsync(notification);
            });

        public async ValueTask<Notification> RemoveNotificationByIdAsync(Guid notificationId) =>
            await TryCatch(async () =>
            {
                ValidateNotificationId(notificationId);
                Notification maybeNotification = await this.storageBroker.SelectNotificationByIdAsync(notificationId);
                ValidateStorageNotification(maybeNotification, notificationId);
                return await this.storageBroker.DeleteNotificationAsync(maybeNotification);
            });
    }
}
