//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

namespace Sheenam.Blazor.Models.Foundations.RentalContracts
{
    public static class ContractStatusExtensions
    {
        public static string ToUzbek(this ContractStatus status) => status switch
        {
            ContractStatus.Active => "Faol",
            ContractStatus.Expired => "Muddati tugagan",
            ContractStatus.Terminated => "Bekor qilingan",
            _ => "Noma'lum"
        };
    }
}