//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Homes
{
    public class HomeService : IHomeService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public HomeService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Home> AddHomeAsync(Home home)
        {
            try
            {
                ValidateHomeOnAdd(home);
                return await this.storageBroker.InsertHomeAsync(home);
            }
            catch (HomeValidationException homeValidationException)
            {
                this.loggingBroker.LogError(homeValidationException);
                throw;
            }
        }

        public IQueryable<Home> RetrieveAllHomes() =>
            this.storageBroker.SelectAllHomes();
        public async ValueTask<Home> RetrieveHomeByIdAsync(Guid homeId) =>
            await this.storageBroker.SelectHomeByIdAsync(homeId);

        private void ValidateHomeOnAdd(Home home)
        {
            if (home is null)
            {
                throw new HomeValidationException(
                    new NullHomeException());
            }

            if (home.Id == Guid.Empty)
            {
                throw new HomeValidationException(
                    new InvalidHomeException(nameof(Home.Id), "Id is required."));
            }

            if (home.HostId == Guid.Empty)
            {
                throw new HomeValidationException(
                    new InvalidHomeException(nameof(Home.HostId), "HostId is required."));
            }

            if (string.IsNullOrWhiteSpace(home.Address))
            {
                throw new HomeValidationException(
                    new InvalidHomeException(nameof(Home.Address), "Address is required."));
            }

            if (home.Price <= 0)
            {
                throw new HomeValidationException(
                    new InvalidHomeException(nameof(Home.Price), "Price must be greater than zero."));
            }

            if (home.Area <= 0)
            {
                throw new HomeValidationException(
                    new InvalidHomeException(nameof(Home.Area), "Area must be greater than zero."));
            }
        }
    }
}
