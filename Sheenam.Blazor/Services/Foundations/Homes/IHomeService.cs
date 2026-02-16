//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Homes;

namespace Sheenam.Blazor.Services.Foundations.Homes
{
    public interface IHomeService
    {
        ValueTask<Home> AddHomeAsync(Home home);
        ValueTask<List<Home>> RetrieveAllHomesAsync();
        ValueTask<Home> RetrieveHomeByIdAsync(Guid homeId);
        ValueTask<Home> ModifyHomeAsync(Home home);
        ValueTask<Home> RemoveHomeByIdAsync(Guid homeId);
    }
}