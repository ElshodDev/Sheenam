//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Services.Brokers;

namespace Sheenam.Blazor.Services.Foundations.Guests
{
    public class GuestService : IGuestService
    {
        private readonly IApiBroker apiBroker;

        public GuestService(IApiBroker apiBroker)
        {
            this.apiBroker = apiBroker;
        }

        public async ValueTask<Guest> AddGuestAsync(Guest guest) =>
            await this.apiBroker.PostGuestAsync(guest);

        public async ValueTask<List<Guest>> RetrieveAllGuestsAsync() =>
            await this.apiBroker.GetAllGuestsAsync();

        public async ValueTask<Guest> RetrieveGuestByIdAsync(Guid guestId) =>
            await this.apiBroker.GetGuestByIdAsync(guestId);

        public async ValueTask<Guest> ModifyGuestAsync(Guest guest) =>
            await this.apiBroker.PutGuestAsync(guest.Id, guest);

        public async ValueTask<Guest> RemoveGuestByIdAsync(Guid guestId) =>
            await this.apiBroker.DeleteGuestByIdAsync(guestId);
    }
}
