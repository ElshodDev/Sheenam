//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Users.Exceptions
{
    public class LockedUserException : Xeption
    {
        public LockedUserException(Exception innerException)
            : base(message: "User is locked, please try again later.", innerException)
        { }
    }
}