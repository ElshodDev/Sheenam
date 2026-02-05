//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.AIs.Exceptions
{
    public class NullAiTextException : Xeption
    {
        public NullAiTextException()
            : base(message: "AI text is null.")
        { }
    }
}
