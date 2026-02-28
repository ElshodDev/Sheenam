//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.HomeRequests;

namespace Sheenam.Blazor.Services.Foundations.HomeRequests
{
    public interface IHomeRequestService
    {
        ValueTask<HomeRequest> AddHomeRequestAsync(HomeRequest homeRequest);
        ValueTask<IQueryable<HomeRequest>> RetrieveAllHomeRequestsAsync();
        ValueTask<HomeRequest> RetrieveHomeRequestByIdAsync(Guid homeRequestId);
        ValueTask<HomeRequest> ModifyHomeRequestAsync(HomeRequest homeRequest);
        ValueTask<HomeRequest> RemoveHomeRequestByIdAsync(Guid homeRequestId);
    }
}