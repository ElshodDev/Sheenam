//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Homes.Exceptions
{
    public class InvalidHomeException : Xeption
    {
        public InvalidHomeException()
            : base(message: "Invalid home. Please correct the errors and try again.")
        { }
    }
}
