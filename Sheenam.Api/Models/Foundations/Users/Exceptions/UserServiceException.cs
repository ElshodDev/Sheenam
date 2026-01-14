//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Users.Exceptions
{
    public class UserServiceException : Xeption
    {
        public UserServiceException(Exception innerException)
            : base(message: "User service error occurred, contact support.",
                  innerException)
        { }
    }
}