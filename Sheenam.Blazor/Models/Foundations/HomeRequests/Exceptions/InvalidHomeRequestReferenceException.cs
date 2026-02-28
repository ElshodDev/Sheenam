//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.HomeRequests.Exceptions
{
    public class InvalidHomeRequestReferenceException : Xeption
    {
        public InvalidHomeRequestReferenceException(Exception innerException)
            : base(message: "Invalid home request reference error occurred.", innerException)
        { }
    }
}