//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.HomeRequests;

namespace Sheenam.Blazor.Services.Foundations.HomeRequests
{
    public partial class HomeRequestService : IHomeRequestService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public HomeRequestService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<HomeRequest> AddHomeRequestAsync(HomeRequest homeRequest) =>
            await TryCatch(async () =>
            {
                ValidateHomeRequestOnAdd(homeRequest);
                return await this.apiBroker.PostHomeRequestAsync(homeRequest);
            });

        public async ValueTask<IQueryable<HomeRequest>> RetrieveAllHomeRequestsAsync() =>
            await TryCatch(async () =>
            {
                var homeRequests = await this.apiBroker.GetAllHomeRequestsAsync();
                return homeRequests.AsQueryable();
            });

        public async ValueTask<HomeRequest> RetrieveHomeRequestByIdAsync(Guid homeRequestId) =>
            await TryCatch(async () =>
            {
                ValidateHomeRequestId(homeRequestId);
                return await this.apiBroker.GetHomeRequestByIdAsync(homeRequestId);
            });

        public async ValueTask<HomeRequest> ModifyHomeRequestAsync(HomeRequest homeRequest) =>
            await TryCatch(async () =>
            {
                ValidateHomeRequestOnModify(homeRequest);
                return await this.apiBroker.PutHomeRequestAsync(homeRequest);
            });

        public async ValueTask<HomeRequest> RemoveHomeRequestByIdAsync(Guid homeRequestId) =>
            await TryCatch(async () =>
            {
                ValidateHomeRequestId(homeRequestId);
                return await this.apiBroker.DeleteHomeRequestByIdAsync(homeRequestId);
            });
    }
}