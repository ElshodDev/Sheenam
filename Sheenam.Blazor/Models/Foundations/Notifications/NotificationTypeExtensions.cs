//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
namespace Sheenam.Blazor.Models.Foundations.Notifications
{
    public static class NotificationTypeExtensions
    {
        public static string ToUzbek(this NotificationType type) => type switch
        {
            NotificationType.HomeRequestUpdate => "Uy so'rovi yangilandi",
            NotificationType.PaymentReminder => "To'lov eslatmasi",
            NotificationType.NewOffer => "Yangi taklif",
            NotificationType.ContractExpiring => "Shartnoma tugayapti",
            NotificationType.SaleOfferReceived => "Sotuv taklifi keldi",
            NotificationType.PropertySold => "Mulk sotildi",
            _ => "Noma'lum"
        };
    }
}