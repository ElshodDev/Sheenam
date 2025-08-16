//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Guests.Exceptions
{
    public class GuestDependecyValidationException :Xeption
    {
        public GuestDependecyValidationException(Xeption innerException)
         :base(message:"Guest dependency validation error occured, fix the errors and try again",
              innerException)
        { }
    }
}
