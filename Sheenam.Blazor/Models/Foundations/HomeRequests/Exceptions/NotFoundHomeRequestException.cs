//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.HomeRequests.Exceptions
{
    public class NotFoundHomeRequestException : Xeption
    {
        public NotFoundHomeRequestException(Guid homeRequestId)
            : base(message: $"Home request not found with id: {homeRequestId}.")
        { }
    }
}