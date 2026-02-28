//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.HomeRequests;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<HomeRequest> PostHomeRequestAsync(HomeRequest homeRequest);
        ValueTask<List<HomeRequest>> GetAllHomeRequestsAsync();
        ValueTask<HomeRequest> GetHomeRequestByIdAsync(Guid homeRequestId);
        ValueTask<HomeRequest> PutHomeRequestAsync(HomeRequest homeRequest);
        ValueTask<HomeRequest> DeleteHomeRequestByIdAsync(Guid homeRequestId);
    }
}