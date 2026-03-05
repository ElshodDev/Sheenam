//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.SavedSearches;
using Sheenam.Api.Models.Foundations.SavedSearches.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.SavedSearches
{
    public partial class SavedSearchService
    {
        private void ValidateSavedSearchOnAdd(SavedSearch savedSearch)
        {
            ValidateSavedSearchNotNull(savedSearch);

            Validate(
                (Rule: IsInvalid(savedSearch.Id), Parameter: nameof(SavedSearch.Id)),
                (Rule: IsInvalid(savedSearch.UserId), Parameter: nameof(SavedSearch.UserId)),
                (Rule: IsInvalid(savedSearch.Name), Parameter: nameof(SavedSearch.Name)),
                (Rule: IsInvalid(savedSearch.CreatedDate), Parameter: nameof(SavedSearch.CreatedDate)));
        }

        private static void ValidateSavedSearchId(Guid savedSearchId) =>
            Validate((Rule: IsInvalid(savedSearchId), Parameter: nameof(SavedSearch.Id)));

        private static void ValidateStorageSavedSearch(
            SavedSearch maybeSavedSearch, Guid savedSearchId)
        {
            if (maybeSavedSearch is null)
                throw new NotFoundSavedSearchException(savedSearchId);
        }

        private static void ValidateSavedSearchNotNull(SavedSearch savedSearch)
        {
            if (savedSearch is null)
                throw new NullSavedSearchException();
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidSavedSearchException = new InvalidSavedSearchException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                    invalidSavedSearchException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
            }

            invalidSavedSearchException.ThrowIfContainsErrors();
        }
    }
}
