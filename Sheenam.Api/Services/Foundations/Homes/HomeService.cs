//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Homes;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Homes
{
    public class HomeService : IHomeService
    {
        private readonly IStorageBroker storageBroker;

        public HomeService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Home> AddHomeAsync(Home home) =>
           await this.storageBroker.InsertHomeAsync(home);

    }
}
