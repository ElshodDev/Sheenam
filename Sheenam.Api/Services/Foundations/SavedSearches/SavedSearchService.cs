//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.SavedSearches;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.SavedSearches
{
    public partial class SavedSearchService : ISavedSearchService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public SavedSearchService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<SavedSearch> AddSavedSearchAsync(SavedSearch savedSearch) =>
            TryCatch(async () =>
            {
                ValidateSavedSearchOnAdd(savedSearch);
                return await this.storageBroker.InsertSavedSearchAsync(savedSearch);
            });

        public IQueryable<SavedSearch> RetrieveAllSavedSearches() =>
            TryCatch(() => this.storageBroker.SelectAllSavedSearches());

        public IQueryable<SavedSearch> RetrieveSavedSearchesByUserId(Guid userId) =>
            TryCatch(() => this.storageBroker
                .SelectAllSavedSearches()
                .Where(ss => ss.UserId == userId));

        public ValueTask<SavedSearch> RetrieveSavedSearchByIdAsync(Guid savedSearchId) =>
            TryCatch(async () =>
            {
                ValidateSavedSearchId(savedSearchId);
                SavedSearch maybeSavedSearch =
                    await this.storageBroker.SelectSavedSearchByIdAsync(savedSearchId);
                ValidateStorageSavedSearch(maybeSavedSearch, savedSearchId);
                return maybeSavedSearch;
            });

        public ValueTask<SavedSearch> RemoveSavedSearchByIdAsync(Guid savedSearchId) =>
            TryCatch(async () =>
            {
                ValidateSavedSearchId(savedSearchId);
                SavedSearch maybeSavedSearch =
                    await this.storageBroker.SelectSavedSearchByIdAsync(savedSearchId);
                ValidateStorageSavedSearch(maybeSavedSearch, savedSearchId);
                return await this.storageBroker.DeleteSavedSearchAsync(maybeSavedSearch);
            });
    }
}
