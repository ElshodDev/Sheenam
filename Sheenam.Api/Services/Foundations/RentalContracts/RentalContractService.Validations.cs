//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.RentalContracts;
using Sheenam.Api.Models.Foundations.RentalContracts.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.RentalContracts
{
    public partial class RentalContractService
    {
        private void ValidateRentalContractOnAdd(RentalContract rentalContract)
        {
            ValidateRentalContractIsNotNull(rentalContract);

            Validate(
                (Rule: IsInvalid(rentalContract.HomeRequestId), Parameter: nameof(RentalContract.HomeRequestId)),
                (Rule: IsInvalid(rentalContract.GuestId), Parameter: nameof(RentalContract.GuestId)),
                (Rule: IsInvalid(rentalContract.HostId), Parameter: nameof(RentalContract.HostId)),
                (Rule: IsInvalid(rentalContract.HomeId), Parameter: nameof(RentalContract.HomeId)),
                (Rule: IsInvalid(rentalContract.StartDate), Parameter: nameof(RentalContract.StartDate)),
                (Rule: IsInvalid(rentalContract.EndDate), Parameter: nameof(RentalContract.EndDate)),
                (Rule: IsInvalid(rentalContract.MonthlyRent), Parameter: nameof(RentalContract.MonthlyRent)),
                (Rule: IsInvalid(rentalContract.SecurityDeposit), Parameter: nameof(RentalContract.SecurityDeposit)),
                (Rule: IsInvalid(rentalContract.Terms), Parameter: nameof(RentalContract.Terms)),
                (Rule: IsInvalid(rentalContract.Status), Parameter: nameof(RentalContract.Status))
            );
        }

        private void ValidateRentalContractOnModify(RentalContract rentalContract)
        {
            ValidateRentalContractIsNotNull(rentalContract);

            Validate(
                (Rule: IsInvalid(rentalContract.Id), Parameter: nameof(RentalContract.Id)),
                (Rule: IsInvalid(rentalContract.HomeRequestId), Parameter: nameof(RentalContract.HomeRequestId)),
                (Rule: IsInvalid(rentalContract.GuestId), Parameter: nameof(RentalContract.GuestId)),
                (Rule: IsInvalid(rentalContract.HostId), Parameter: nameof(RentalContract.HostId)),
                (Rule: IsInvalid(rentalContract.HomeId), Parameter: nameof(RentalContract.HomeId)),
                (Rule: IsInvalid(rentalContract.StartDate), Parameter: nameof(RentalContract.StartDate)),
                (Rule: IsInvalid(rentalContract.EndDate), Parameter: nameof(RentalContract.EndDate)),
                (Rule: IsInvalid(rentalContract.MonthlyRent), Parameter: nameof(RentalContract.MonthlyRent)),
                (Rule: IsInvalid(rentalContract.SecurityDeposit), Parameter: nameof(RentalContract.SecurityDeposit)),
                (Rule: IsInvalid(rentalContract.Terms), Parameter: nameof(RentalContract.Terms)),
                (Rule: IsInvalid(rentalContract.Status), Parameter: nameof(RentalContract.Status))
            );
        }

        public void ValidateRentalContractId(Guid rentalContractId) =>
            Validate((Rule: IsInvalid(rentalContractId), Parameter: nameof(RentalContract.Id)));

        private static void ValidateStorageRentalContract(RentalContract maybeRentalContract, Guid rentalContractId)
        {
            if (maybeRentalContract is null)
            {
                throw new NotFoundRentalContractException(rentalContractId);
            }
        }

        private void ValidateRentalContractIsNotNull(RentalContract rentalContract)
        {
            if (rentalContract is null)
            {
                throw new NullRentalContractException();
            }
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

        private static dynamic IsInvalid(decimal amount) => new
        {
            Condition = amount <= 0,
            Message = "Amount is required"
        };

        private static dynamic IsInvalid<T>(T value) where T : Enum => new
        {
            Condition = Enum.IsDefined(typeof(T), value) is false,
            Message = "Value is invalid"
        };

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