//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Hosts;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public partial class HostService
    {
        private void ValidateHostOnAdd(Host host)
        {
            ValidateHostNotNull(host);

            Validate(
              // ✅ REMOVED: (Rule: IsInvalid(host.Id), Parameter: nameof(Host.Id))
              // Id will be generated in service, no need to validate on Add
              (Rule: IsInvalid(host.FirstName), Parameter: nameof(Host.FirstName)),
              (Rule: IsInvalid(host.LastName), Parameter: nameof(Host.LastName)),
              (Rule: IsInvalid(host.DateOfBirth), Parameter: nameof(Host.DateOfBirth)),
              (Rule: IsInvalid(host.Email), Parameter: nameof(Host.Email)),
              (Rule: IsInvalid(host.Gender), Parameter: nameof(Host.Gender)));
        }

        private void ValidateHostOnModify(Host host)
        {
            ValidateHostNotNull(host);

            Validate(
              // ✅ For Modify, we DO validate Id because it must exist
              (Rule: IsInvalid(host.Id), Parameter: nameof(Host.Id)),
              (Rule: IsInvalid(host.FirstName), Parameter: nameof(Host.FirstName)),
              (Rule: IsInvalid(host.LastName), Parameter: nameof(Host.LastName)),
              (Rule: IsInvalid(host.DateOfBirth), Parameter: nameof(Host.DateOfBirth)),
              (Rule: IsInvalid(host.Email), Parameter: nameof(Host.Email)),
              (Rule: IsInvalid(host.Gender), Parameter: nameof(Host.Gender)));
        }

        private void ValidateHostNotNull(Host host)
        {
            if (host is null)
            {
                throw new NullHostException();
            }
        }

        private static dynamic IsInvalid(Guid Id) => new
        {
            Condition = Id == Guid.Empty,
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

        private static dynamic IsInvalid(GenderType gender) => new
        {
            Condition = Enum.IsDefined(typeof(GenderType), gender) is false,
            Message = "Invalid gender value"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidHostException = new InvalidHostException();

            foreach (var (rule, parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidHostException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidHostException.ThrowIfContainsErrors();
        }
    }
}