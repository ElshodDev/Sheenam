//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Auth.Exceptions
{
    public class InvalidAuthInputException : Xeption
    {
        public InvalidAuthInputException()
            : base(message: "Auth input is invalid.")
        { }
    }
}