//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.Homes
{
    public partial class HomeService
    {
        private void ValidateHomeOnAdd(Home home)
        {
            ValidateHomeNotNull(home);

            Validate(
                (Rule: IsInvalid(home.Id), Parameter: nameof(Home.Id)),
                (Rule: IsInvalidHostId(home.HostId), Parameter: nameof(Home.HostId)),
                (Rule: IsInvalid(home.Address), Parameter: nameof(Home.Address)),
                (Rule: IsInvalid(home.AdditionalInfo), Parameter: nameof(Home.AdditionalInfo)),
                (Rule: IsInvalidBedrooms(home.NumberOfBedrooms), Parameter: nameof(Home.NumberOfBedrooms)),
                (Rule: IsInvalidBathrooms(home.NumberOfBathrooms), Parameter: nameof(Home.NumberOfBathrooms)),
                (Rule: IsInvalid(home.Area), Parameter: nameof(Home.Area)),
                (Rule: IsInvalid(home.Price), Parameter: nameof(Home.Price))
            );
        }

        private void ValidateHomeNotNull(Home home)
        {
            if (home is null)
            {
                throw new NullHomeException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is Required"
        };

        private static dynamic IsInvalidHostId(Guid hostId) => new
        {
            Condition = hostId == Guid.Empty,
            Message = "Host Id is Required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is Required"
        };

        private static dynamic IsInvalidBedrooms(int number) => new
        {
            Condition = number <= 0,
            Message = "Number of bedrooms must be greater than 0"
        };

        private static dynamic IsInvalidBathrooms(int number) => new
        {
            Condition = number <= 0,
            Message = "Number of bathrooms must be greater than 0"
        };

        private static dynamic IsInvalid(double number) => new
        {
            Condition = number <= 0,
            Message = "Area must be greater than 0"
        };

        private static dynamic IsInvalid(decimal number) => new
        {
            Condition = number <= 0,
            Message = "Price must be greater than 0"
        };

        private static dynamic IsInvalid(HouseType house) => new
        {
            Condition = Enum.IsDefined(typeof(HouseType), house) is false,
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
