//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================
using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Guests.Exceptions
{
    public class FailedGuestDependencyException : Xeption
    {
        public FailedGuestDependencyException(Xeption innerException)
            : base(message: "Failed guest dependency error occurred, contact support.", innerException)
        { }
    }
}
