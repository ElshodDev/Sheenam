//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Homes.Exceptions
{
    public class AlreadyExistHomeException : Xeption
    {
        public AlreadyExistHomeException()
            : base(message: "Home with the same details already exists.")
        { }
    }
}
