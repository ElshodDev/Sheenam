//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Guests;

namespace Sheenam.Blazor.Services.Brokers
{
    public partial class ApiBroker
    {
        private const string GuestsEndpoint = "api/guests";

        public async ValueTask<Guest> PostGuestAsync(Guest guest) =>
            await this.PostAsync(GuestsEndpoint, guest);

        public async ValueTask<List<Guest>> GetAllGuestsAsync() =>
            await this.GetAsync<List<Guest>>(GuestsEndpoint);

        public async ValueTask<Guest> GetGuestByIdAsync(Guid guestId) =>
            await this.GetAsync<Guest>($"{GuestsEndpoint}/{guestId}");

        public async ValueTask<Guest> PutGuestAsync(Guid guestId, Guest guest) =>
            await this.PutAsync($"{GuestsEndpoint}/{guestId}", guest);

        public async ValueTask<Guest> DeleteGuestByIdAsync(Guid guestId) =>
            await this.DeleteAsync<Guest>($"{GuestsEndpoint}/{guestId}");
    }
}
