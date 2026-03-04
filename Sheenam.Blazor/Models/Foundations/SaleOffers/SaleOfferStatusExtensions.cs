//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
namespace Sheenam.Blazor.Models.Foundations.SaleOffers
{
    public static class SaleOfferStatusExtensions
    {
        public static string ToUzbek(this SaleOfferStatus status) => status switch
        {
            SaleOfferStatus.Pending => "Kutilmoqda",
            SaleOfferStatus.Accepted => "Qabul qilindi",
            SaleOfferStatus.Rejected => "Rad etildi",
            SaleOfferStatus.Cancelled => "Bekor qilindi",
            _ => "Noma'lum"
        };
    }
}