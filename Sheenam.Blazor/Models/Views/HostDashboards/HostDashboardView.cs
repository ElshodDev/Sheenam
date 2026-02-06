//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================
namespace Sheenam.Blazor.Models.Views.HostDashboards
{
    public class HostDashboardView
    {
        public Guid HostId { get; set; }
        public List<dynamic> Houses { get; set; } // Hozircha dynamic, keyin HomeView qilsak bo'ladi
        public decimal TotalEarnings { get; set; }
    }
}