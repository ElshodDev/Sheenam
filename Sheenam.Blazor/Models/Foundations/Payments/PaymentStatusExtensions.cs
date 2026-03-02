//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

namespace Sheenam.Blazor.Models.Foundations.Payments
{
    public static class PaymentStatusExtensions
    {
        public static string ToUzbek(this PaymentStatus status) => status switch
        {
            PaymentStatus.Pending => "Kutilmoqda",
            PaymentStatus.Completed => "Bajarilgan",
            PaymentStatus.Failed => "Muvaffaqiyatsiz",
            PaymentStatus.Refunded => "Qaytarilgan",
            _ => "Noma'lum"
        };
    }
}