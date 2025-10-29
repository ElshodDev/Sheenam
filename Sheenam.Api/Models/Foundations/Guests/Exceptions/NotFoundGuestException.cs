//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Guests.Exceptions
{
    public class NotFoundGuestException : Xeption
    {
        public NotFoundGuestException(Guid id)
            : base(message: $"Couldn't find guest with id: {id}.") { }
        public NotFoundGuestException(string message) : base(message) { }

    }
}
