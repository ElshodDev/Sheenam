//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Guests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Guest> InserGuestAsync(Guest guest);
        ValueTask<Guest> SelectGuestByIdAsync(Guid id);
        IQueryable<Guest> SelectAllGuests();
        ValueTask<Guest> UpdateGuestAsync(Guest guest);
    }
}
