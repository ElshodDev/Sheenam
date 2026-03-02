//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

namespace Sheenam.Blazor.Models.Foundations.Payments
{
    public static class PaymentMethodExtensions
    {
        public static string ToUzbek(this PaymentMethod method) => method switch
        {
            PaymentMethod.Cash => "Naqd pul",
            PaymentMethod.Card => "Karta",
            PaymentMethod.BankTransfer => "Bank o'tkazmasi",
            PaymentMethod.OnlinePayment => "Onlayn to'lov",
            _ => "Noma'lum"
        };
    }
}