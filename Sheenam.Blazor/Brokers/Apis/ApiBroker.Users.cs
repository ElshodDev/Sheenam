//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Users;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        public async ValueTask<LoginResponse> PostLoginAsync(LoginRequest loginRequest) =>
          await this.PostAsync<LoginRequest, LoginResponse>("api/users/login", loginRequest);

        public async ValueTask<User> PostUserAsync(User user) =>
            await this.PostAsync<User, User>("api/users", user);
    }
}
