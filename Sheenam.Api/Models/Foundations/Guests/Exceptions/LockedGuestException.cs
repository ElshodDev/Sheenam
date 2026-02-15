using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Guests.Exceptions
{
    public class LockedGuestException : Xeption
    {
        public LockedGuestException(Exception innerException)
            : base(message: "Locked guest record error occurred. Please try again.", innerException)
        { }
    }
}
