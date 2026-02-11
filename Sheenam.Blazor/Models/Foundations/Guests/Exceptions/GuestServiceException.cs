using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Guests.Exceptions
{
    public class GuestServiceException : Xeption
    {
        public GuestServiceException(Xeption innerException)
            : base(message: "Guest service error occurred, contact support.", innerException)
        { }
    }
}
