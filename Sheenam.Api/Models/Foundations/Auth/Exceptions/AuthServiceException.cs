//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Auth.Exceptions
{
    public class AuthServiceException : Xeption
    {
        public AuthServiceException(Xeption innerException)
            : base(message: "Auth service error occurred, contact support.",
                  innerException)
        { }
    }
}