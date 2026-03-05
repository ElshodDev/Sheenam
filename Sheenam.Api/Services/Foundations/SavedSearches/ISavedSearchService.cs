//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.SavedSearches;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.SavedSearches
{
    public interface ISavedSearchService
    {
        ValueTask<SavedSearch> AddSavedSearchAsync(SavedSearch savedSearch);
        IQueryable<SavedSearch> RetrieveAllSavedSearches();
        IQueryable<SavedSearch> RetrieveSavedSearchesByUserId(Guid userId);
        ValueTask<SavedSearch> RetrieveSavedSearchByIdAsync(Guid savedSearchId);
        ValueTask<SavedSearch> RemoveSavedSearchByIdAsync(Guid savedSearchId);
    }
}
