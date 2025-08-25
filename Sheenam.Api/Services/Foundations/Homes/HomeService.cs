//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Homes;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Homes
{
    public partial class HomeService : IHomeService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public HomeService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Home> AddHomeAsync(Home home) =>
        TryCatch(async () =>
        {
            ValidateHomeOnAdd(home);

            return await this.storageBroker.InsertHomeAsync(home);
        });
    }
}

