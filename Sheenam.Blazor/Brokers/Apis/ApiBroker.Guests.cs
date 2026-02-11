//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System.Collections.Generic;
using System.Threading.Tasks;
using Sheenam.Blazor.Models.Foundations.Guests;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string GuestsRelativeUrl = "api/guests";

        public async ValueTask<Guest> PostGuestAsync(Guest guest) =>
            await this.PostContentAsync(GuestsRelativeUrl, guest);

        public async ValueTask<List<Guest>> GetAllGuestsAsync() =>
            await this.GetContentAsync<List<Guest>>(GuestsRelativeUrl);
    }
}
