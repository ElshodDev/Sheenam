//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Guests.Exceptions
{
    public class GuestDependecyException :Xeption
    {
        public GuestDependecyException(Xeption innerException)
            : base(message: "Guest dependency error occured, contact support",
                  innerException)
        { }
    }
}
