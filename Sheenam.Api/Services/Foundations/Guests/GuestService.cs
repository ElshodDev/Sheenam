//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;
using System;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Guests
{
    public class GuestService : IGuestService
    {
        private readonly IStorageBroker storageBroker;

        public GuestService(IStorageBroker storageBroker)=>
        this.storageBroker = storageBroker;

        public async ValueTask<Guest> AddGuestAsync(Guest guest)
        {
            if (guest is null)
            {
                var nullGuestException = new NullGuestException();

                throw new GuestValidationException(nullGuestException);
            }

            return await this.storageBroker.InserGuestAsync(guest);
        }

    }
}
