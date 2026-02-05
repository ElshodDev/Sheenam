//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

namespace Sheenam.Api.Models.Foundations.Homes
{
    public class HomeFilter
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? NumberOfRooms { get; set; }
        public HouseType? HouseType { get; set; }
    }
}
