//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;
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
        public async ValueTask<Guest> ModifyGuestAsync(Guest guest) 
        {
            ValidateGuestOnModify(guest);

            Guest trackedGuest = await this.storageBroker.SelectGuestByIdAsync(guest.Id);

            ValidateStorageGuest(trackedGuest, guest.Id);

            trackedGuest.FirstName = guest.FirstName;
            trackedGuest.LastName = guest.LastName;
            trackedGuest.DateOfBirth = guest.DateOfBirth;
            trackedGuest.Email = guest.Email;
            trackedGuest.PhoneNumber = guest.PhoneNumber;
            trackedGuest.Address = guest.Address;
            trackedGuest.Gender = guest.Gender;

            return await this.storageBroker.UpdateGuestAsync(trackedGuest);
        }
        private void ValidateGuestOnModify(Guest guest)
        {
            if (guest == null)
                throw new GuestValidationException("Guest cannot be null.");


            if (string.IsNullOrWhiteSpace(guest.FirstName))
                throw new GuestValidationException("Guest first name is required.");

            if (string.IsNullOrWhiteSpace(guest.LastName))
                throw new GuestValidationException("Guest last name is required.");
        }
        private void ValidateStorageGuest(Guest maybeGuest, Guid Id)
        {
            if (maybeGuest == null)
                throw new NotFoundGuestException($"Guest with id {Id} not found.");
        }
    }
}
