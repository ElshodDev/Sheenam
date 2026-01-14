//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Users.Exceptions
{
    public class NotFoundUserException : Xeption
    {
        public NotFoundUserException(Guid userId)
            : base(message: $"Couldn't find user with id: {userId}.")
        { }

        public NotFoundUserException(string email)
            : base(message: $"Couldn't find user with email: {email}.")
        { }
    }
}