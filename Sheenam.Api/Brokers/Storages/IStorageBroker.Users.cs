//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<User> InsertUserAsync(User user);
        IQueryable<User> SelectAllUsers();
        ValueTask<User> SelectUserByIdAsync(Guid userId);
        ValueTask<User> SelectUserByEmailAsync(string email);
        ValueTask<User> UpdateUserAsync(User user);
        ValueTask<User> DeleteUserAsync(User user);
    }
}