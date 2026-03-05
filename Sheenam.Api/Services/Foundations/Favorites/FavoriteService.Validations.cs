//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Favorites;
using Sheenam.Api.Models.Foundations.Favorites.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.Favorites
{
    public partial class FavoriteService
    {
        private void ValidateFavoriteOnAdd(Favorite favorite)
        {
            ValidateFavoriteNotNull(favorite);

            Validate(
                (Rule: IsInvalid(favorite.Id), Parameter: nameof(Favorite.Id)),
                (Rule: IsInvalid(favorite.UserId), Parameter: nameof(Favorite.UserId)),
                (Rule: IsInvalid(favorite.CreatedDate), Parameter: nameof(Favorite.CreatedDate)));
        }

        private static void ValidateFavoriteId(Guid favoriteId) =>
            Validate((Rule: IsInvalid(favoriteId), Parameter: nameof(Favorite.Id)));

        private static void ValidateStorageFavorite(Favorite maybeFavorite, Guid favoriteId)
        {
            if (maybeFavorite is null)
                throw new NotFoundFavoriteException(favoriteId);
        }

        private static void ValidateFavoriteNotNull(Favorite favorite)
        {
            if (favorite is null)
                throw new NullFavoriteException();
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidFavoriteException = new InvalidFavoriteException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                    invalidFavoriteException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
            }

            invalidFavoriteException.ThrowIfContainsErrors();
        }
    }
}