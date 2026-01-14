//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Users;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Auth
{
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user
        /// </summary>
        ValueTask<User> RegisterAsync(User user, string password);

        /// <summary>
        /// Authenticates user and returns JWT token
        /// </summary>
        ValueTask<string> LoginAsync(string email, string password);

        /// <summary>
        /// Generates JWT token for authenticated user
        /// </summary>
        string GenerateJwtToken(User user);
    }
}