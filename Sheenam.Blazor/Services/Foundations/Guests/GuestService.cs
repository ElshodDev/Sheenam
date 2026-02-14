//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.Guests;

namespace Sheenam.Blazor.Services.Foundations.Guests
{
    public partial class GuestService : IGuestService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public GuestService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;

        }

        public async ValueTask<Guest> AddGuestAsync(Guest guest) =>
    await TryCatch(async () =>
    {
        ValidateGuestOnAdd(guest);

        return await this.apiBroker.PostGuestAsync(guest);
    });
        public IQueryable<Guest> RetrieveAllGuests() =>
      TryCatch(() =>
          this.apiBroker.GetAllGuestsAsync().GetAwaiter().GetResult().AsQueryable());
    }
}
