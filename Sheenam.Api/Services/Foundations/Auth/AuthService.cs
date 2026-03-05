//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.Extensions.Configuration;
using Sheenam.Api.Brokers.Tokens;
using Sheenam.Api.Models.Foundations.Auth.Exceptions;
using Sheenam.Api.Models.Foundations.Users;
using Sheenam.Api.Services.Foundations.Users;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Auth
{
    public partial class AuthService : IAuthService
    {
        private readonly IUserService userService;
        private readonly ITokenBroker tokenBroker;
        private readonly IConfiguration configuration;

        public AuthService(
            IUserService userService,
            ITokenBroker tokenBroker,
            IConfiguration configuration)
        {
            this.userService = userService;
            this.tokenBroker = tokenBroker;
            this.configuration = configuration;
        }

        public ValueTask<User> RegisterAsync(User user, string password) =>
        TryCatch(async () =>
        {
            ValidateRegisterInput(user, password);

            return await this.userService.RegisterUserAsync(user, password);
        });

        public ValueTask<string> LoginAsync(string email, string password) =>
        TryCatch(async () =>
        {
            ValidateLoginInput(email, password);

            User user = await this.userService.RetrieveUserByEmailAsync(email);

            bool isPasswordValid = this.userService.VerifyPassword(password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new InvalidCredentialsException();
            }

            return this.tokenBroker.GenerateToken(user);
        });
    }
}