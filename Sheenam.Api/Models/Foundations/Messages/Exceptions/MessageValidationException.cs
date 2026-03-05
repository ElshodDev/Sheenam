//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Messages.Exceptions
{
    public class MessageValidationException : Xeption
    {
        public MessageValidationException(Xeption innerException)
            : base(message: "Message validation error occurred.", innerException) { }
    }
}
