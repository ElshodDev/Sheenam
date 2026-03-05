//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Messages.Exceptions
{
    public class NotFoundMessageException : Xeption
    {
        public NotFoundMessageException(Guid messageId)
            : base(message: $"Could not find message with id: {messageId}.") { }
    }
}
