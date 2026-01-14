//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Auth.Exceptions
{
    public class AuthDependencyValidationException : Xeption
    {
        public AuthDependencyValidationException(Xeption innerException)
            : base(message: "Auth dependency validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}