//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To use  Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Guests;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Guest> InserGuestAsync(Guest guest);
    }
}
