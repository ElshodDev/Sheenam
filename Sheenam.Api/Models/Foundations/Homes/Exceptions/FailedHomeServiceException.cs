//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Homes.Exceptions
{
    public class FailedHomeServiceException : Xeption
    {
        public FailedHomeServiceException(Exception serviceException)
           : base(message: "Home service how error determine",
                serviceException)
        { }
    }
}
