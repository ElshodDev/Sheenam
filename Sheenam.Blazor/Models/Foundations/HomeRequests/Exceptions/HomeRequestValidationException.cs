//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

namespace Sheenam.Blazor.Models.Foundations.HomeRequests.Exceptions
{
    public class HomeRequestValidationException : Exception
    {
        public HomeRequestValidationException(Exception innerException)
            : base("Home request validation error occurred.", innerException) { }
    }
}
