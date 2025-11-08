//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.HomeRequests.Exceptions
{
    public class AlreadyExistsHomeRequestException : Xeption
    {
        public AlreadyExistsHomeRequestException(Exception innerException)
            : base(message: "Home request with the same key already exists.", innerException)
        { }
    }
}
