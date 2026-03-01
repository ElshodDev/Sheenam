//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

namespace Sheenam.Blazor.Models.Foundations.HomeRequests
{
    public static class HomeRequestStatusExtensions
    {
        public static string ToUzbek(this HomeRequestStatus status) => status switch
        {
            HomeRequestStatus.Pending => "Kutilmoqda",
            HomeRequestStatus.Approved => "Tasdiqlangan",
            HomeRequestStatus.Rejected => "Rad etilgan",
            HomeRequestStatus.Cancelled => "Bekor qilingan",
            _ => "Noma'lum"
        };
    }
}