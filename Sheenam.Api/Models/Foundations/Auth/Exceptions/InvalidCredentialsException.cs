//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Auth.Exceptions
{
    public class InvalidCredentialsException : Xeption
    {
        public InvalidCredentialsException()
            : base(message: "Invalid email or password.")
        { }
    }
}