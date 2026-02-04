//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Notification> Notifications { get; set; }

        public async ValueTask<Notification> InsertNotificationAsync(Notification notification)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Notification> notificationEntityEntry =
                await broker.Notifications.AddAsync(notification);
            await broker.SaveChangesAsync();
            return notificationEntityEntry.Entity;
        }

        public IQueryable<Notification> SelectAllNotifications() =>
            this.Notifications;

        public async ValueTask<Notification> SelectNotificationByIdAsync(Guid notificationId)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.Notifications.FindAsync(notificationId);
        }

        public async ValueTask<Notification> UpdateNotificationAsync(Notification notification)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Notification> notificationEntityEntry =
                broker.Notifications.Update(notification);
            await broker.SaveChangesAsync();
            return notificationEntityEntry.Entity;
        }

        public async ValueTask<Notification> DeleteNotificationAsync(Notification notification)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Notification> notificationEntityEntry =
                broker.Notifications.Remove(notification);
            await broker.SaveChangesAsync();
            return notificationEntityEntry.Entity;
        }

        public async ValueTask<Notification> SelectMostRecentNotificationByUserIdAsync(Guid userId)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.Notifications
                .Where(notification => notification.UserId == userId)
                .OrderByDescending(notification => notification.CreatedDate)
                .FirstOrDefaultAsync();
        }

        public async ValueTask<int> CountUnreadNotificationsByUserIdAsync(Guid userId)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.Notifications
                .Where(notification => notification.UserId == userId && !notification.IsRead)
                .CountAsync();
        }

        public async ValueTask<Notification> MarkNotificationAsReadAsync(Guid notificationId)
        {
            using var broker = new StorageBroker(this.configuration);
            Notification notification = await broker.Notifications.FindAsync(notificationId);
            if (notification != null && !notification.IsRead)
            {
                notification.IsRead = true;
                EntityEntry<Notification> notificationEntityEntry =
                    broker.Notifications.Update(notification);
                await broker.SaveChangesAsync();
                return notificationEntityEntry.Entity;
            }
            return notification;
        }

        public async ValueTask<Notification> MarkNotificationAsUnreadAsync(Guid notificationId)
        {
            using var broker = new StorageBroker(this.configuration);
            Notification notification = await broker.Notifications.FindAsync(notificationId);
            if (notification != null && notification.IsRead)
            {
                notification.IsRead = false;
                EntityEntry<Notification> notificationEntityEntry =
                    broker.Notifications.Update(notification);
                await broker.SaveChangesAsync();
                return notificationEntityEntry.Entity;
            }
            return notification;
        }
    }
}
