//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.Homes;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<Home> PostHomeAsync(Home home);
        ValueTask<List<Home>> GetAllHomesAsync();
        ValueTask<Home> GetHomeByIdAsync(Guid homeId);
        ValueTask<Home> PutHomeAsync(Home home);
        ValueTask<Home> DeleteHomeByIdAsync(Guid homeId);
    }
}