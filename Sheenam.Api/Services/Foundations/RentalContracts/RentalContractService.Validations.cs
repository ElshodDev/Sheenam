//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.RentalContracts;
using Sheenam.Api.Models.Foundations.RentalContracts.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.RentalContacts
{
    public partial class RentalContractService
    {
        private void ValidateRentalContractOnAdd(RentalContract rentalContract)
        {
            ValidateRentalContractNotNull(rentalContract);

            Validate(
                (Rule: IsInvalid(rentalContract.Id, nameof(RentalContract.Id)), Parameter: nameof(RentalContract.Id)),
                (Rule: IsInvalid(rentalContract.HomeRequestId, nameof(RentalContract.HomeRequestId)), Parameter: nameof(RentalContract.HomeRequestId)),
                (Rule: IsInvalid(rentalContract.GuestId, nameof(RentalContract.GuestId)), Parameter: nameof(RentalContract.GuestId)),
                (Rule: IsInvalid(rentalContract.HostId, nameof(RentalContract.HostId)), Parameter: nameof(RentalContract.HostId)),
                (Rule: IsInvalid(rentalContract.HomeId, nameof(RentalContract.HomeId)), Parameter: nameof(RentalContract.HomeId)),
                (Rule: IsInvalid(rentalContract.StartDate, nameof(RentalContract.StartDate)), Parameter: nameof(RentalContract.StartDate)),
                (Rule: IsInvalid(rentalContract.EndDate, nameof(RentalContract.EndDate)), Parameter: nameof(RentalContract.EndDate)),
                (Rule: IsInvalid(rentalContract.MonthlyRent, nameof(RentalContract.MonthlyRent)), Parameter: nameof(RentalContract.MonthlyRent)),
                (Rule: IsInvalid(rentalContract.SecurityDeposit, nameof(RentalContract.SecurityDeposit)), Parameter: nameof(RentalContract.SecurityDeposit)),
                (Rule: IsInvalid(rentalContract.Terms, nameof(RentalContract.Terms)), Parameter: nameof(RentalContract.Terms)),
                (Rule: IsInvalid(rentalContract.Status, nameof(RentalContract.Status)), Parameter: nameof(RentalContract.Status)));
        }

        private static dynamic IsInvalid(Guid id, string parameterName) => new
        {
            Condition = id == Guid.Empty,
            Message = $"{parameterName} is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date, string parameterName) => new
        {
            Condition = date == default,
            Message = $"{parameterName} is required"
        };

        private static dynamic IsInvalid(decimal amount, string parameterName) => new
        {
            Condition = amount <= 0,
            Message = $"{parameterName} is required"
        };

        private static dynamic IsInvalid(string text, string parameterName) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = $"{parameterName} is required"
        };
        private static dynamic IsInvalid(ContractStatus status, string parameterName) => new
        {
            Condition = Enum.IsDefined(typeof(ContractStatus), status) is false,
            Message = "Status is invalid."
        };
        private void ValidateRentalContractNotNull(RentalContract rentalContract)
        {
            if (rentalContract is null)
            {
                throw new NullRentalContractException();
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidRentalContractException = new InvalidRentalContractException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidRentalContractException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidRentalContractException.ThrowIfContainsErrors();
        }
    }
}
