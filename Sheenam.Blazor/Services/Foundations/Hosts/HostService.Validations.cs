//===================================================
//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Models.Foundations.Hosts.Exceptions;
using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Services.Foundations.Hosts
{
    public partial class HostService
    {
        private static void ValidateHostOnAdd(HostModel host)
        {
            ValidateHostNotNull(host);

            Validate(
                (Rule: IsInvalid(host.Id), Parameter: nameof(HostModel.Id)),
                (Rule: IsInvalid(host.FirstName), Parameter: nameof(HostModel.FirstName)),
                (Rule: IsInvalid(host.LastName), Parameter: nameof(HostModel.LastName)),
                (Rule: IsInvalid(host.DateOfBirth), Parameter: nameof(HostModel.DateOfBirth)),
                (Rule: IsInvalid(host.Email), Parameter: nameof(HostModel.Email)),
                (Rule: IsInvalid(host.PhoneNumber), Parameter: nameof(HostModel.PhoneNumber)),
                (Rule: IsInvalid(host.Gender), Parameter: nameof(HostModel.Gender)));
        }

        private static void ValidateHostOnModify(HostModel host)
        {
            ValidateHostNotNull(host);

            Validate(
                (Rule: IsInvalid(host.Id), Parameter: nameof(HostModel.Id)),
                (Rule: IsInvalid(host.FirstName), Parameter: nameof(HostModel.FirstName)),
                (Rule: IsInvalid(host.LastName), Parameter: nameof(HostModel.LastName)),
                (Rule: IsInvalid(host.DateOfBirth), Parameter: nameof(HostModel.DateOfBirth)),
                (Rule: IsInvalid(host.Email), Parameter: nameof(HostModel.Email)),
                (Rule: IsInvalid(host.PhoneNumber), Parameter: nameof(HostModel.PhoneNumber)),
                (Rule: IsInvalid(host.Gender), Parameter: nameof(HostModel.Gender)));
        }

        private static void ValidateHostId(Guid hostId) =>
            Validate((Rule: IsInvalid(hostId), Parameter: nameof(HostModel.Id)));

        private static void ValidateHostNotNull(HostModel host)
        {
            if (host is null)
            {
                throw new NullHostException();
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

        private static dynamic IsInvalid(GenderType gender) => new
        {
            Condition = Enum.IsDefined(gender) is false,
            Message = "Value is invalid"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidHostException = new InvalidHostException();

            foreach ((dynamic rule, string parameter) in validations)
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
