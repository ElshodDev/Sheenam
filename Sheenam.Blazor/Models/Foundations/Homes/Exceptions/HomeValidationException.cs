//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Homes.Exceptions
{
    public class HomeValidationException : Xeption
    {
        public HomeValidationException(Exception innerException)
            : base(message: "Home validation error occurred, fix the errors and try again.", innerException)
        { }
    }
}
