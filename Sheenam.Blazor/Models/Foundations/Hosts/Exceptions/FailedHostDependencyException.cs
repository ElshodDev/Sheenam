//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Hosts.Exceptions
{
    public class FailedHostDependencyException : Xeption
    {
        public FailedHostDependencyException(Xeption innerException)
            : base(message: "Failed host dependency error occurred, contact support.", innerException)
        { }
    }
}
