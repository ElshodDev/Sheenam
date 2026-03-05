//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Messages.Exceptions
{
    public class MessageServiceException : Xeption
    {
        public MessageServiceException(Xeption innerException)
            : base(message: "Message service error occurred.", innerException) { }
    }
}
