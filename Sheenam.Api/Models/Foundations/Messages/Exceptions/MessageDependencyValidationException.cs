//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Messages.Exceptions
{
    public class MessageDependencyValidationException : Xeption
    {
        public MessageDependencyValidationException(Xeption innerException)
            : base(message: "Message dependency validation error occurred.", innerException) { }
    }
}
