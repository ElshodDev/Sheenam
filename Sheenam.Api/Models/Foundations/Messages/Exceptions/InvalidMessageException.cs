//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Messages.Exceptions
{
    public class InvalidMessageException : Xeption
    {
        public InvalidMessageException()
            : base(message: "Message is invalid.") { }
    }
}
