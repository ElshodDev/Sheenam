//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

namespace Sheenam.Blazor.Models.Foundations.Homes.Exceptions
{
    public class HomeServiceException : Exception
    {
        public HomeServiceException(Exception innerException)
            : base(message: "Home service error occurred, contact support.", innerException)
        { }
    }
}