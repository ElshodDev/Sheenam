//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.RentalContracts;
using System;
using System.Data;

namespace Sheenam.Api.Services.Foundations.RentalContacts
{
    public partial class RentalContractService
    {
        private void ValidateRentalContractOnAdd(RentalContract rentalContract)
        {
            ValidateRentalContractNotNull(rentalContract);

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
               (Rule: IsInvalid(rentalContract.Status), Parameter: nameof(RentalContract.Status)),
               (Rule: IsInvalid(rentalContract.SignedDate), Parameter: nameof(RentalContract.SignedDate)),
               (Rule: IsInvalid(rentalContract.CreatedDate), Parameter: nameof(RentalContract.CreatedDate)),
               (Rule: IsInvalid(rentalContract.UpdatedDate), Parameter: nameof(RentalContract.UpdatedDate)));
        }
        private void ValidateRentalContractNotNull(RentalContract rentalContract)
        {
            if (rentalContract is null)
            {
                throw new DataException("Rental Contract is null");
            }
        }
        private static dynamic IsInvalid(Guid Id) => new
        {
            Condition = Id == Guid.Empty,
            Message = "Id is Required"
        };
        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is Required"
        };
        private static dynamic IsInvalid(decimal amount) => new
        {
            Condition = amount <= 0,
            Message = "Amount must be greater than zero"
        };
        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is Required"
        };
        private static dynamic IsInvalid(ContractStatus status) => new
        {
            Condition = Enum.IsDefined(typeof(ContractStatus), status) is false,
            Message = "Value is invalid"
        };
        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidRentalContractException = new DataException();
            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidRentalContractException.Data.Add(
                        parameter,
                        rule.Message);
                }
            }
            if (invalidRentalContractException.Data.Count > 0)
            {
                throw invalidRentalContractException;
            }
        }
    }
}
