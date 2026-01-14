//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Auth.Exceptions
{
    public class AuthValidationException : Xeption
    {
        public AuthValidationException(Xeption innerException)
            : base(message: "Auth validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}