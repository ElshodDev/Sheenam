//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Homes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Homes
{
    public interface IHomeService
    {
        ValueTask<Home> AddHomeAsync(Home home);
        ValueTask<Home> RetrieveHomeByIdAsync(Guid homeId);
        IQueryable<Home> RetrieveAllHomes();
        ValueTask<Home> ModifyHomeAsync(Home home);
        ValueTask<Home> RemoveHomeByIdAsync(Guid homeId);
    }
}
