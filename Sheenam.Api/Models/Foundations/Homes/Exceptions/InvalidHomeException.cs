//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Homes.Exceptions
{
    public class InvalidHomeException : Xeption
    {
        public InvalidHomeException(string parameterName, string parameterValue)
            : base($"Invalid home error occurred, please fix the errors and try again. {parameterName}: {parameterValue}")
        { }
    }
}
