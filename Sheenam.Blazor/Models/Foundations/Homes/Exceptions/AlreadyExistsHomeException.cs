//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Homes.Exceptions
{
    public class AlreadyExistsHomeException : Xeption
    {
        public AlreadyExistsHomeException(Exception innerException)
            : base(message: "Home with the same id already exists.", innerException)
        { }
    }
}
