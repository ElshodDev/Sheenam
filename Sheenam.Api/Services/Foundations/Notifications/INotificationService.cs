//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Notifications
{
    public interface INotificationService
    {
        ValueTask<Notification> AddNotificationAsync(Notification notification);
        IQueryable<Notification> RetrieveAllNotifications();
        ValueTask<Notification> RetrieveNotificationByIdAsync(Guid notificationId);
        ValueTask<Notification> ModifyNotificationAsync(Notification notification);
        ValueTask<Notification> RemoveNotificationByIdAsync(Guid notificationId);
    }
}
