//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Users
{
    public partial class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public UserService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<User> RegisterUserAsync(User user, string password) =>
        TryCatch(async () =>
        {
            ValidateUserOnRegister(user, password);

            user.Id = Guid.NewGuid();
            user.PasswordHash = HashPassword(password);
            user.CreatedDate = DateTimeOffset.UtcNow;
            user.UpdatedDate = DateTimeOffset.UtcNow;

            return await this.storageBroker.InsertUserAsync(user);
        });

        public IQueryable<User> RetrieveAllUsers() =>
            TryCatch(() => this.storageBroker.SelectAllUsers());

        public ValueTask<User> RetrieveUserByIdAsync(Guid userId) =>
        TryCatch(async () =>
        {
            ValidateUserId(userId);

            User maybeUser = await this.storageBroker.SelectUserByIdAsync(userId);

            ValidateStorageUser(maybeUser, userId);

            return maybeUser;
        });

        public ValueTask<User> RetrieveUserByEmailAsync(string email) =>
        TryCatch(async () =>
        {
            ValidateEmail(email);

            User maybeUser = await this.storageBroker.SelectUserByEmailAsync(email);

            ValidateStorageUserByEmail(maybeUser, email);

            return maybeUser;
        });

        public ValueTask<User> ModifyUserAsync(User user) =>
        TryCatch(async () =>
        {
            ValidateUserOnModify(user);

            User maybeUser = await this.storageBroker.SelectUserByIdAsync(user.Id);

            ValidateStorageUser(maybeUser, user.Id);

            user.UpdatedDate = DateTimeOffset.UtcNow;

            return await this.storageBroker.UpdateUserAsync(user);
        });

        public ValueTask<User> RemoveUserByIdAsync(Guid userId) =>
        TryCatch(async () =>
        {
            ValidateUserId(userId);

            User maybeUser = await this.storageBroker.SelectUserByIdAsync(userId);

            ValidateStorageUser(maybeUser, userId);

            return await this.storageBroker.DeleteUserAsync(maybeUser);
        });

        public bool VerifyPassword(string password, string passwordHash)
        {
            ValidatePasswordOnVerify(password, passwordHash);

            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}