//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Models.Foundations.Users;

namespace Sheenam.Blazor.Services.Foundations.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IApiBroker apiBroker;

        public AuthService(IApiBroker apiBroker) =>
            this.apiBroker = apiBroker;

        public async ValueTask<LoginResponse> LoginAsync(LoginRequest loginRequest) =>
            await this.apiBroker.PostLoginAsync(loginRequest);

        public async ValueTask<User> RegisterAsync(User user, string password)
        {
            return await this.apiBroker.PostUserAsync(user);
        }
    }
}