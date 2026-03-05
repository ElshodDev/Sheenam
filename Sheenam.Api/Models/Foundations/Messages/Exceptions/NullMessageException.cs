//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Messages.Exceptions
{
    public class NullMessageException : Xeption
    {
        public NullMessageException()
            : base(message: "Message is null.") { }
    }
}
