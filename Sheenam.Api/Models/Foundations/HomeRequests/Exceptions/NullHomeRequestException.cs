//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.HomeRequests.Exceptions
{
    public class NullHomeRequestException : Xeption
    {
        public NullHomeRequestException()
            : base(message: "The home request is null.")
        { }
    }
}
