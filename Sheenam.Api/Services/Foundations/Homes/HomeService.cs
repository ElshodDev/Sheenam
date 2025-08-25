//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;
using Sheenam.Api.Brokers.Storages;
using System.Threading.Tasks;
using Sheenam.Api.Brokers.Loggings;

namespace Sheenam.Api.Services.Foundations.Homes
{
    public class HomeService : IHomeService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public HomeService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Home> AddHomeAsync(Home home)
        {
            if (home is null)
            {
                var nullHomeException = new NullHomeException();
                var homeValidationException = new HomeValidationException(nullHomeException);

                this.loggingBroker.LogError(homeValidationException);

                throw homeValidationException;
            }

            return await this.storageBroker.InsertHomeAsync(home);
        }
    }
}
