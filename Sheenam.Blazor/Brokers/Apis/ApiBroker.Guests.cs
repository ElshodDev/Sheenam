//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Guests;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string GuestsRelativeUrl = "api/guests";

        public async ValueTask<Guest> PostGuestAsync(Guest guest) =>
            await this.PostContentAsync("api/guests", guest);

        public async ValueTask<List<Guest>> GetAllGuestsAsync() =>
            await this.GetContentAsync<List<Guest>>(GuestsRelativeUrl);

        public async ValueTask<Guest> PutGuestAsync(Guest guest) =>
            await this.PutContentAsync(GuestsRelativeUrl, guest);

        public async ValueTask<Guest> DeleteGuestByIdAsync(Guid guestId) =>
    await this.DeleteContentAsync<Guest>($"{GuestsRelativeUrl}/{guestId}");
    }
}
