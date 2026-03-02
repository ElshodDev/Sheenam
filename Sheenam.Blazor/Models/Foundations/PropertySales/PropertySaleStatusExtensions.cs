//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

namespace Sheenam.Blazor.Models.Foundations.PropertySales
{
    public static class PropertySaleStatusExtensions
    {
        public static string ToUzbek(this PropertySaleStatus status) => status switch
        {
            PropertySaleStatus.Available => "Mavjud",
            PropertySaleStatus.Reserved => "Band qilingan",
            PropertySaleStatus.Sold => "Sotilgan",
            PropertySaleStatus.Removed => "Olib tashlangan",
            _ => "Noma'lum"
        };
    }
}