//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Users;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<LoginResponse> PostLoginAsync(LoginRequest loginRequest);
        ValueTask<User> PostUserAsync(User user);
    }
}
