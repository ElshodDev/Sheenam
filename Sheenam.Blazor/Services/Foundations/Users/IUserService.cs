//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Users;

namespace Sheenam.Blazor.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}