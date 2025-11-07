//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.HomeRequests;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.HomeRequests
{
    public class HomeRequestService : IHomeRequestService
    {
        private readonly IStorageBroker storageBroker;
        public HomeRequestService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<HomeRequest> AddHomeRequestAsync(HomeRequest homeRequest) =>
            this.storageBroker.InsertHomeRequestAsync(homeRequest);
    }
}
