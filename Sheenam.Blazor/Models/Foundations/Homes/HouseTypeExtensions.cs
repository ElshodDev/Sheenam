//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

namespace Sheenam.Blazor.Models.Foundations.Homes
{
    public static class HouseTypeExtensions
    {
        public static string ToUzbek(this HouseType type) => type switch
        {
            HouseType.Flat => "Kvartira",
            HouseType.Bungalow => "Bungalo",
            HouseType.Duplex => "Dupleks",
            HouseType.Villa => "Villa",
            HouseType.Townhouse => "Shahar uyi",
            HouseType.Studio => "Studiya",
            HouseType.Penthouse => "Penthouse",
            HouseType.Cottage => "Kottedj",
            HouseType.Other => "Boshqa",
            _ => "Noma'lum"
        };
    }
}