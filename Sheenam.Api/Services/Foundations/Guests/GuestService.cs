//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Guests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Guests
{
    public partial class GuestService : IGuestService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public GuestService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Guest> AddGuestAsync(Guest guest)
        {
            return TryCatch(async () =>
           {
               ValidateGuestOnAdd(guest);

               return await this.storageBroker.InserGuestAsync(guest);

           });
        }

        public IQueryable<Guest> RetrieveAllGuests()
        {
            return this.storageBroker.SelectAllGuests();
        }
        public async ValueTask<Guest> RetrieveGuestByIdAsync(Guid Id)
        {
            Guest maybeGuest = await this.storageBroker.SelectGuestByIdAsync(Id);

            if (maybeGuest is null)
            {
                throw new Models.Foundations.Guests.Exceptions.NotFoundGuestException(Id);
            }

            return maybeGuest;
        }
    }
}
