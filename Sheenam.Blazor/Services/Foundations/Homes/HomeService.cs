//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.Homes;

namespace Sheenam.Blazor.Services.Foundations.Homes
{
    public partial class HomeService : IHomeService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public HomeService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Home> AddHomeAsync(Home home) =>
            await TryCatch(async () =>
            {
                ValidateHomeOnAdd(home);
                return await this.apiBroker.PostHomeAsync(home);
            });

        public async ValueTask<List<Home>> RetrieveAllHomesAsync() =>
            await TryCatch(async () => await this.apiBroker.GetAllHomesAsync());

        public async ValueTask<Home> RetrieveHomeByIdAsync(Guid homeId) =>
            await TryCatch(async () => await this.apiBroker.GetHomeByIdAsync(homeId));

        public async ValueTask<Home> ModifyHomeAsync(Home home) =>
            await TryCatch(async () => await this.apiBroker.PutHomeAsync(home));

        public async ValueTask<Home> RemoveHomeByIdAsync(Guid homeId) =>
            await TryCatch(async () => await this.apiBroker.DeleteHomeByIdAsync(homeId));
    }
}