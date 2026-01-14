//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Users
{
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user with hashed password
        /// </summary>
        ValueTask<User> RegisterUserAsync(User user, string password);

        /// <summary>
        /// Retrieves all users
        /// </summary>
        IQueryable<User> RetrieveAllUsers();

        /// <summary>
        /// Retrieves user by ID
        /// </summary>
        ValueTask<User> RetrieveUserByIdAsync(Guid userId);

        /// <summary>
        /// Retrieves user by email (for login)
        /// </summary>
        ValueTask<User> RetrieveUserByEmailAsync(string email);

        /// <summary>
        /// Updates existing user
        /// </summary>
        ValueTask<User> ModifyUserAsync(User user);

        /// <summary>
        /// Deletes user by ID
        /// </summary>
        ValueTask<User> RemoveUserByIdAsync(Guid userId);

        /// <summary>
        /// Verifies password for login
        /// </summary>
        bool VerifyPassword(string password, string passwordHash);
    }
}