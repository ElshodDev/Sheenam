//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Hosts.Exceptions
{
    public class InvalidHostReferenceException : Xeption
    {
        public InvalidHostReferenceException(Exception innerException)
            : base(message: "Invalid host reference error occurred.", innerException)
        { }
    }
}
