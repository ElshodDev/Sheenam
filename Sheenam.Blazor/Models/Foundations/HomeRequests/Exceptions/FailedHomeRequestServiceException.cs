//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.HomeRequests.Exceptions
{
    public class FailedHomeRequestServiceException : Xeption
    {
        public FailedHomeRequestServiceException(Exception innerException)
            : base(message: "Failed home request service error occurred.", innerException)
        { }
    }
}