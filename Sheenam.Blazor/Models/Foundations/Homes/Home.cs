//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================
using System.ComponentModel.DataAnnotations;

namespace Sheenam.Blazor.Models.Foundations.Homes
{
    public class Home
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid HostId { get; set; }

        [Required(ErrorMessage = "Manzil majburiy!")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Manzil 5-200 ta belgi bo'lishi kerak")]
        public string Address { get; set; }

        [StringLength(500, ErrorMessage = "Qo'shimcha ma'lumot 500 ta belgidan oshmasligi kerak")]
        public string? AdditionalInfo { get; set; }

        public bool IsVacant { get; set; }

        public bool IsPetAllowed { get; set; }

        public bool IsShared { get; set; }

        [Required(ErrorMessage = "Yotoq xonalar soni majburiy!")]
        [Range(0, 20, ErrorMessage = "Yotoq xonalar soni 0-20 orasida bo'lishi kerak")]
        public int NumberOfBedrooms { get; set; }

        [Required(ErrorMessage = "Vanna xonalar soni majburiy!")]
        [Range(0, 10, ErrorMessage = "Vanna xonalar soni 0-10 orasida bo'lishi kerak")]
        public int NumberOfBathrooms { get; set; }

        [Required(ErrorMessage = "Maydon majburiy!")]
        [Range(10, 10000, ErrorMessage = "Maydon 10-10000 m² orasida bo'lishi kerak")]
        public double Area { get; set; }

        [Required(ErrorMessage = "Narx majburiy!")]
        [Range(10000, 100000000, ErrorMessage = "Narx 10,000-100,000,000 so'm orasida bo'lishi kerak")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Uy turi majburiy!")]
        public HouseType Type { get; set; }
    }
}