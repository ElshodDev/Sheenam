//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Users;

namespace Sheenam.Blazor.Services.Foundations.Auth
{
    public interface IAuthService
    {
        ValueTask<User> RegisterAsync(User user, string password);
    }
}
