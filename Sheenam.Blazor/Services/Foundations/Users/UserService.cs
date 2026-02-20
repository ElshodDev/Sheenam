//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.Users;

namespace Sheenam.Blazor.Services.Foundations.Users
{
    public partial class UserService : IUserService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public UserService(IApiBroker apiBroker, ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            return await this.apiBroker.PostLoginAsync(loginRequest);
        }
    }
}