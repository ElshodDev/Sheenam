//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================
using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Guests.Exceptions
{
    public class AlreadyExistsGuestException : Xeption
    {
        public AlreadyExistsGuestException(Xeption innerException)
            : base(message: "Guest with the same id already exists.", innerException)
        { }
    }
}
