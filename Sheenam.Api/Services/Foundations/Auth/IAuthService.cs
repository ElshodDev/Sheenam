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
        ValueTask<User> RegisterAsync(User user, string password);
        ValueTask<string> LoginAsync(string email, string password);
    }
}