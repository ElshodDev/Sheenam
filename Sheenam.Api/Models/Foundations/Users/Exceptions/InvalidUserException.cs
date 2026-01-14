//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Users.Exceptions
{
    public class InvalidUserException : Xeption
    {
        public InvalidUserException()
            : base(message: "User is invalid.")
        { }
    }
}