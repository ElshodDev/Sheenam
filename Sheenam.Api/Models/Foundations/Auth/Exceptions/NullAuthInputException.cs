//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Auth.Exceptions
{
    public class NullAuthInputException : Xeption
    {
        public NullAuthInputException(string parameterName)
            : base(message: $"{parameterName} is null.")
        { }
    }
}