//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.HomeRequests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.HomeRequests
{
    public interface IHomeRequestService
    {
        ValueTask<HomeRequest> AddHomeRequestAsync(HomeRequest homeRequest);
        IQueryable<HomeRequest> RetrieveAllHomeRequests();
        ValueTask<HomeRequest> RetrieveHomeRequestByIdAsync(Guid homeRequestId);
        ValueTask<HomeRequest> ModifyHomeRequestAsync(HomeRequest homeRequest);
        ValueTask<HomeRequest> RemoveHomeRequestByIdAsync(Guid homeRequestId);

        ValueTask<HomeRequest> ApproveHomeRequestAsync(Guid homeRequestId);
        ValueTask<HomeRequest> RejectHomeRequestAsync(Guid homeRequestId, string rejectionReason = null);
        ValueTask<HomeRequest> CancelHomeRequestAsync(Guid homeRequestId);
        IQueryable<HomeRequest> RetrieveHomeRequestsByStatusAsync(HomeRequestStatus status);
    }
}
