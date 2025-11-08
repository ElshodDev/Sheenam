//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.HomeRequests;
using Sheenam.Api.Models.Foundations.HomeRequests.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.HomeRequests
{
    public partial class HomeRequestService : IHomeRequestService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        public HomeRequestService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<HomeRequest> AddHomeRequestAsync(HomeRequest homeRequest) =>
             TryCatch(async () =>
             {
                 ValidateHomeRequestOnAdd(homeRequest);

                 return await this.storageBroker.InsertHomeRequestAsync(homeRequest);
             });
        public IQueryable<HomeRequest> RetrieveAllHomeRequests() =>
         this.storageBroker.SelectAllHomeRequests();

        public async ValueTask<HomeRequest> RetrieveHomeRequestByIdAsync(Guid homeRequestId)
        {
            HomeRequest maybeHomeRequest = await this.storageBroker.SelectHomeRequestByIdAsync(homeRequestId);
            if (maybeHomeRequest is null)
            {
                throw new NotFoundHomeRequestException(homeRequestId);
            }
            return maybeHomeRequest;
        }
    }
}
