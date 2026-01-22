//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.Homes
{
    public partial class HomeService
    {
        private static void ValidateHomeOnAdd(Home home)
        {
            ValidateHomeIsNotNull(home);

            Validate(
                (Rule: IsInvalid(home.Id), Parameter: nameof(Home.Id)),
                (Rule: IsInvalid(home.HostId), Parameter: nameof(Home.HostId)),
                (Rule: IsInvalid(home.Address), Parameter: nameof(Home.Address)),
                (Rule: IsInvalid(home.AdditionalInfo), Parameter: nameof(Home.AdditionalInfo)),
                (Rule: IsInvalid(home.NumberOfBedrooms), Parameter: nameof(Home.NumberOfBedrooms)),
                (Rule: IsInvalid(home.NumberOfBathrooms), Parameter: nameof(Home.NumberOfBathrooms)),
                (Rule: IsInvalid(home.Area), Parameter: nameof(Home.Area)),
                (Rule: IsInvalid(home.Type), Parameter: nameof(Home.Type)),
                (Rule: IsInvalid(home.ListingType), Parameter: nameof(Home.ListingType)),
                (Rule: IsInvalid(home.ListedDate), Parameter: nameof(Home.ListedDate)));

            ValidatePricingBasedOnListingType(home);
        }

        private static void ValidateHomeOnModify(Home home)
        {
            ValidateHomeIsNotNull(home);

            Validate(
                (Rule: IsInvalid(home.Id), Parameter: nameof(Home.Id)),
                (Rule: IsInvalid(home.HostId), Parameter: nameof(Home.HostId)),
                (Rule: IsInvalid(home.Address), Parameter: nameof(Home.Address)),
                (Rule: IsInvalid(home.AdditionalInfo), Parameter: nameof(Home.AdditionalInfo)),
                (Rule: IsInvalid(home.NumberOfBedrooms), Parameter: nameof(Home.NumberOfBedrooms)),
                (Rule: IsInvalid(home.NumberOfBathrooms), Parameter: nameof(Home.NumberOfBathrooms)),
                (Rule: IsInvalid(home.Area), Parameter: nameof(Home.Area)),
                (Rule: IsInvalid(home.Type), Parameter: nameof(Home.Type)),
                (Rule: IsInvalid(home.SecurityDeposit ?? 0), Parameter: nameof(Home.SecurityDeposit)),
                (Rule: IsInvalid(home.ImageUrls), Parameter: nameof(Home.ImageUrls)),
                (Rule: IsInvalid(home.ListingType), Parameter: nameof(Home.ListingType)),
                (Rule: IsInvalid(home.ListedDate), Parameter: nameof(Home.ListedDate)));

            ValidatePricingBasedOnListingType(home);
        }

        private static void ValidateHomeId(Guid homeId) =>
            Validate((Rule: IsInvalid(homeId), Parameter: nameof(Home.Id)));

        private static void ValidateStorageHome(Home maybeHome, Guid homeId)
        {
            if (maybeHome is null)
            {
                throw new NotFoundHomeException(homeId);
            }
        }

        private static void ValidateHomeIsNotNull(Home home)
        {
            if (home is null)
            {
                throw new NullHomeException();
            }
        }

        private static void ValidatePricingBasedOnListingType(Home home)
        {
            var invalidHomeException = new InvalidHomeException();

            if (home.ListingType == ListingType.ForRent || home.ListingType == ListingType.Both)
            {
                if (!home.MonthlyRent.HasValue || home.MonthlyRent.Value <= 0)
                {
                    invalidHomeException.UpsertDataList(
                        key: nameof(Home.MonthlyRent),
                        value: "Monthly rent is required for rental listings");
                }
            }

            if (home.ListingType == ListingType.ForSale || home.ListingType == ListingType.Both)
            {
                if (!home.SalePrice.HasValue || home.SalePrice.Value <= 0)
                {
                    invalidHomeException.UpsertDataList(
                        key: nameof(Home.SalePrice),
                        value: "Sale price is required for sale listings");
                }
            }

            invalidHomeException.ThrowIfContainsErrors();
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

        private static dynamic IsInvalid(int number) => new
        {
            Condition = number < 0,
            Message = "Number cannot be negative"
        };

        private static dynamic IsInvalid(double number) => new
        {
            Condition = number <= 0,
            Message = "Number must be greater than zero"
        };

        private static dynamic IsInvalid(decimal number) => new
        {
            Condition = number <= 0,
            Message = "Number must be greater than zero"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsInvalid(HouseType type) => new
        {
            Condition = Enum.IsDefined(typeof(HouseType), type) == false,
            Message = "Value is invalid"
        };

        private static dynamic IsInvalid(ListingType listingType) => new
        {
            Condition = Enum.IsDefined(typeof(ListingType), listingType) == false,
            Message = "Value is invalid"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidHomeException = new InvalidHomeException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidHomeException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidHomeException.ThrowIfContainsErrors();
        }
    }
}