//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.HomeRequests.Exceptions
{
    public class AlreadyExistsHomeRequestException : Xeption
    {
        public AlreadyExistsHomeRequestException(Xeption innerException)
            : base(message: "Home request with the same id already exists.", innerException)
        { }
    }
}