//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Notification> Notifications { get; set; }

        public async ValueTask<Notification> InsertNotificationAsync(Notification notification) =>
             await InsertAsync(notification);

        public IQueryable<Notification> SelectAllNotifications() =>
           SelectAll<Notification>();
        public async ValueTask<Notification> SelectNotificationByIdAsync(Guid notificationId) =>
            await SelectAsync<Notification>(notificationId);
        public async ValueTask<Notification> UpdateNotificationAsync(Notification notification) =>
            await UpdateAsync(notification);


        public async ValueTask<Notification> DeleteNotificationAsync(Notification notification) =>
            await DeleteAsync(notification);

    }
}
