//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.SavedSearches;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<SavedSearch> InsertSavedSearchAsync(SavedSearch savedSearch);
        IQueryable<SavedSearch> SelectAllSavedSearches();
        ValueTask<SavedSearch> SelectSavedSearchByIdAsync(Guid savedSearchId);
        ValueTask<SavedSearch> DeleteSavedSearchAsync(SavedSearch savedSearch);
    }
}
