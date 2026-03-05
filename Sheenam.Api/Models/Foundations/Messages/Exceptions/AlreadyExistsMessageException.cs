//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Messages.Exceptions
{
    public class AlreadyExistsMessageException : Xeption
    {
        public AlreadyExistsMessageException(Exception innerException)
            : base(message: "Message already exists.", innerException) { }
    }
}
