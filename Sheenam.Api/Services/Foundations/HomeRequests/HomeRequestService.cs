//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.HomeRequests;
using Sheenam.Api.Models.Foundations.HomeRequests.Exceptions;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.HomeRequests
{
    public class HomeRequestService : IHomeRequestService
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

        public async ValueTask<HomeRequest> AddHomeRequestAsync(HomeRequest homeRequest)
        {
            try
            {
                if (homeRequest is null)
                {
                    throw new NullHomeRequestException();
                }
                return await this.storageBroker.InsertHomeRequestAsync(homeRequest);
            }
            catch (NullHomeRequestException nullHomeRequestException)
            {
                var homeRequestValidationException =
                    new HomeRequestValidationException(nullHomeRequestException);
                this.loggingBroker.LogError(homeRequestValidationException);
                throw homeRequestValidationException;
            }
        }
    }
}
