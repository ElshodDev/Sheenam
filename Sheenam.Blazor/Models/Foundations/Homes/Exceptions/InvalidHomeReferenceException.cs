//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Homes.Exceptions
{
    public class InvalidHomeReferenceException : Xeption
    {
        public InvalidHomeReferenceException(Xeption innerException)
            : base(message: "Invalid home reference error occurred, please fix the errors and try again.", innerException)
        { }
    }
}
