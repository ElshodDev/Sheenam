//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.Notifications;
namespace Sheenam.Blazor.Services.Foundations.Notifications
{
    public partial interface INotificationService
    {
        ValueTask<Notification> AddNotificationAsync(Notification notification);
        ValueTask<IQueryable<Notification>> RetrieveAllNotificationsAsync();
        ValueTask<Notification> RetrieveNotificationByIdAsync(Guid notificationId);
        ValueTask<Notification> ModifyNotificationAsync(Notification notification);
        ValueTask<Notification> RemoveNotificationByIdAsync(Guid notificationId);
    }
}