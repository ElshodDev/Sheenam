//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Hosts.Exceptions
{
    public class AlreadyExistsHostException : Xeption
    {
        public AlreadyExistsHostException(Exception innerException)
           : base(message: "Host already exists", innerException)
        { }
    }
}
