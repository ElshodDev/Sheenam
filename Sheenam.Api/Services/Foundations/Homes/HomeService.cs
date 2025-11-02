//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.Data.SqlClient;
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

        public async ValueTask<Home> ModifyHomeAsync(Home home)
        {
            try
            {
                if (home is null)
                {
                    throw new NullHomeException();
                }

                ValidateHomeOnModify(home);

                Home maybeHome = await this.storageBroker.SelectHomeByIdAsync(home.Id);

                if (maybeHome is null)
                {
                    throw new NotFoundHomeException(home.Id);
                }

                return await this.storageBroker.UpdateHomeAsync(home);
            }
            catch (InvalidHomeException invalidHomeException)
            {
                var homeValidationException = new HomeValidationException(invalidHomeException);
                this.loggingBroker.LogError(homeValidationException);
                throw homeValidationException;
            }
            catch (NullHomeException nullHomeException)
            {
                var homeValidationException = new HomeValidationException(nullHomeException);
                this.loggingBroker.LogError(homeValidationException);
                throw homeValidationException;
            }
            catch (NotFoundHomeException notFoundHomeException)
            {
                var homeValidationException = new HomeValidationException(notFoundHomeException);
                this.loggingBroker.LogError(homeValidationException);
                throw homeValidationException;
            }
            catch (SqlException sqlException)
            {
                var failedHomeStorageException = new FailedHomeStorageException(sqlException);
                var homeDependencyException = new HomeDependencyException(failedHomeStorageException);

                this.loggingBroker.LogError(homeDependencyException);
                throw homeDependencyException;
            }
            catch (Exception exception)
            {
                var failedHomeServiceException = new FailedHomeServiceException(exception);
                var homeServiceException = new HomeServiceException(failedHomeServiceException);

                this.loggingBroker.LogError(homeServiceException);
                throw homeServiceException;
            }
        }
        public async ValueTask<Home> RemoveHomeByIdAsync(Guid homeId)
        {
            try
            {
                Home maybeHome = await this.storageBroker.SelectHomeByIdAsync(homeId);

                if (maybeHome == null)
                {
                    throw new NotFoundHomeException(homeId);
                }
                Home deletedHome = await this.storageBroker.DeleteHomeByIdAsync(homeId);

                return deletedHome;
            }
            catch (NotFoundHomeException notFoundHomeException)
            {
                throw new HomeValidationException(notFoundHomeException);
            }
            catch (SqlException sqlException)
            {
                throw new HomeDependencyException(
                    new FailedHomeStorageException(sqlException));
            }
            catch (Exception exception)
            {
                throw new HomeServiceException(
                    new FailedHomeServiceException(exception));
            }
        }
        private static void ValidateHomeOnModify(Home home)
        {
            if (home.Id == Guid.Empty)
            {
                throw new InvalidHomeException(nameof(Home.Id), home.Id.ToString());
            }

            if (home.HostId == Guid.Empty)
            {
                throw new InvalidHomeException(nameof(Home.HostId), home.HostId.ToString());
            }

            if (home.Price <= 0)
            {
                throw new InvalidHomeException(nameof(Home.Price), home.Price.ToString());
            }

            if (home.Area <= 0)
            {
                throw new InvalidHomeException(nameof(Home.Area), home.Area.ToString());
            }

            if (home.NumberOfBedrooms < 0)
            {
                throw new InvalidHomeException(nameof(Home.NumberOfBedrooms), home.NumberOfBedrooms.ToString());
            }

            if (home.NumberOfBathrooms < 0)
            {
                throw new InvalidHomeException(nameof(Home.NumberOfBathrooms), home.NumberOfBathrooms.ToString());
            }
        }

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
