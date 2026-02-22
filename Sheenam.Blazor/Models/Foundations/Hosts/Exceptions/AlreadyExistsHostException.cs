//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Hosts.Exceptions
{
    public class AlreadyExistsHostException : Xeption
    {
        public AlreadyExistsHostException(Xeption innerException)
            : base(message: "Host with the same id already exists.", innerException)
        { }
    }
}
