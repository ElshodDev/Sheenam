//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.Notifications;
namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<Notification> PostNotificationAsync(Notification notification);
        ValueTask<List<Notification>> GetAllNotificationsAsync();
        ValueTask<Notification> GetNotificationByIdAsync(Guid notificationId);
        ValueTask<Notification> PutNotificationAsync(Notification notification);
        ValueTask<Notification> DeleteNotificationByIdAsync(Guid notificationId);
    }
}