//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Messages.Exceptions
{
    public class MessageDependencyException : Xeption
    {
        public MessageDependencyException(Xeption innerException)
            : base(message: "Message dependency error occurred.", innerException) { }
    }
}
