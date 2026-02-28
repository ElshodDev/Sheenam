//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.HomeRequests.Exceptions
{
    public class FailedHomeRequestDependencyException : Xeption
    {
        public FailedHomeRequestDependencyException(Exception innerException)
            : base(message: "Failed home request dependency error occurred.", innerException) { }
    }
}