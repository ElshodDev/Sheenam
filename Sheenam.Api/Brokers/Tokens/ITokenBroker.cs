//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Users;

namespace Sheenam.Api.Brokers.Tokens
{
    public interface ITokenBroker
    {
        string GenerateToken(User user);
    }
}