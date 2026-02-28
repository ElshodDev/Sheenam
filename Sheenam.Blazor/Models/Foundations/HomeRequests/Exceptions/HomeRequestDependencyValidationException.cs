//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.HomeRequests.Exceptions
{
    public class HomeRequestDependencyValidationException : Xeption
    {
        public HomeRequestDependencyValidationException(Xeption innerException)
            : base(message: "Home request dependency validation error occurred.", innerException)
        { }
    }
}