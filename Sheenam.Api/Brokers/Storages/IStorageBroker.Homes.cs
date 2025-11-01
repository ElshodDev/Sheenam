//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Homes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {

        ValueTask<Home> InsertHomeAsync(Home home);
        IQueryable<Home> SelectAllHomes();
        ValueTask<Home> SelectHomeByIdAsync(Guid homeId);
    }
}
