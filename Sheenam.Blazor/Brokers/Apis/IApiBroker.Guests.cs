//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System.Collections.Generic;
using System.Threading.Tasks;
using Sheenam.Blazor.Models.Foundations.Guests;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<Guest> PostGuestAsync(Guest guest);
        ValueTask<List<Guest>> GetAllGuestsAsync();
    }
}
