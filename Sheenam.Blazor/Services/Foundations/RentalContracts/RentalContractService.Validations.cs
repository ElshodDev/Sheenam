//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.RentalContracts;
using Sheenam.Blazor.Models.Foundations.RentalContracts.Exceptions;

namespace Sheenam.Blazor.Services.Foundations.RentalContracts
{
    public partial class RentalContractService
    {
        private static void ValidateRentalContractOnAdd(RentalContract rentalContract) =>
            ValidateRentalContractFields(rentalContract);

        private static void ValidateRentalContractOnModify(RentalContract rentalContract) =>
            ValidateRentalContractFields(rentalContract);

        private static void ValidateRentalContractFields(RentalContract rentalContract)
        {
            ValidateRentalContractNotNull(rentalContract);

            Validate(
                (Rule: IsInvalid(rentalContract.Id), Parameter: nameof(RentalContract.Id)),
                (Rule: IsInvalid(rentalContract.HomeRequestId), Parameter: nameof(RentalContract.HomeRequestId)),
                (Rule: IsInvalid(rentalContract.GuestId), Parameter: nameof(RentalContract.GuestId)),
                (Rule: IsInvalid(rentalContract.HostId), Parameter: nameof(RentalContract.HostId)),
                (Rule: IsInvalid(rentalContract.HomeId), Parameter: nameof(RentalContract.HomeId)),
                (Rule: IsInvalid(rentalContract.StartDate), Parameter: nameof(RentalContract.StartDate)),
                (Rule: IsInvalid(rentalContract.EndDate), Parameter: nameof(RentalContract.EndDate)));
        }

        private static void ValidateRentalContractId(Guid rentalContractId) =>
            Validate((Rule: IsInvalid(rentalContractId), Parameter: nameof(RentalContract.Id)));

        private static void ValidateRentalContractNotNull(RentalContract rentalContract)
        {
            if (rentalContract is null)
                throw new NullRentalContractException();
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