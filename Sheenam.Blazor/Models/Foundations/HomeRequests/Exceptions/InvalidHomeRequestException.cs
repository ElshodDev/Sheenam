//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.HomeRequests.Exceptions
{
    public class InvalidHomeRequestException : Xeption
    {
        public InvalidHomeRequestException()
            : base(message: "Invalid home request, fix errors and try again.")
        { }
    }
}