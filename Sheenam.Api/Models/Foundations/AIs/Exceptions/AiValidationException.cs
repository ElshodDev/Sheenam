//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.AIs.Exceptions
{
    public class AiValidationException : Xeption
    {
        public AiValidationException(Xeption innerException)
            : base(message: "AI validation error occurred, fix the errors and try again.", innerException)
        { }
    }
}
