//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.AIs.Exceptions
{
    public class AiServiceException : Xeption
    {
        public AiServiceException(Xeption innerException)
            : base(message: "AI service error occurred, contact support.", innerException)
        { }
    }
}
