//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
namespace Sheenam.Blazor.Models.Foundations.SaleTransactions
{
    public static class TransactionStatusExtensions
    {
        public static string ToUzbek(this TransactionStatus status) => status switch
        {
            TransactionStatus.Pending => "Kutilmoqda",
            TransactionStatus.Completed => "Yakunlandi",
            TransactionStatus.Cancelled => "Bekor qilindi",
            TransactionStatus.Failed => "Muvaffaqiyatsiz",
            _ => "Noma'lum"
        };
    }
}