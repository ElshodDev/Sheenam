//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Notification> InsertNotificationAsync(Notification notification);
        IQueryable<Notification> SelectAllNotifications();
        ValueTask<Notification> SelectNotificationByIdAsync(Guid notificationId);
        ValueTask<Notification> UpdateNotificationAsync(Notification notification);
        ValueTask<Notification> DeleteNotificationAsync(Notification notification);
        ValueTask<Notification> SelectMostRecentNotificationByUserIdAsync(Guid userId);
        ValueTask<int> CountUnreadNotificationsByUserIdAsync(Guid userId);
        ValueTask<Notification> MarkNotificationAsReadAsync(Guid notificationId);
        ValueTask<Notification> MarkNotificationAsUnreadAsync(Guid userId);

    }
}
