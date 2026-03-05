//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Properties;
using Sheenam.Api.Models.Foundations.Properties.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.Properties
{
    public partial class PropertyService
    {
        private void ValidatePropertyOnAdd(Property property)
        {
            ValidatePropertyNotNull(property);

            Validate(
                (Rule: IsInvalid(property.Id), Parameter: nameof(Property.Id)),
                (Rule: IsInvalid(property.AgentId), Parameter: nameof(Property.AgentId)),
                (Rule: IsInvalid(property.Title), Parameter: nameof(Property.Title)),
                (Rule: IsInvalid(property.Address), Parameter: nameof(Property.Address)),
                (Rule: IsInvalid(property.City), Parameter: nameof(Property.City)),
                (Rule: IsInvalid(property.CreatedDate), Parameter: nameof(Property.CreatedDate)),
                (Rule: IsInvalid(property.UpdatedDate), Parameter: nameof(Property.UpdatedDate)));
        }

        private void ValidatePropertyOnModify(Property property)
        {
            ValidatePropertyNotNull(property);

            Validate(
                (Rule: IsInvalid(property.Id), Parameter: nameof(Property.Id)),
                (Rule: IsInvalid(property.AgentId), Parameter: nameof(Property.AgentId)),
                (Rule: IsInvalid(property.Title), Parameter: nameof(Property.Title)),
                (Rule: IsInvalid(property.Address), Parameter: nameof(Property.Address)),
                (Rule: IsInvalid(property.City), Parameter: nameof(Property.City)),
                (Rule: IsInvalid(property.UpdatedDate), Parameter: nameof(Property.UpdatedDate)));
        }

        private static void ValidatePropertyId(Guid propertyId) =>
            Validate((Rule: IsInvalid(propertyId), Parameter: nameof(Property.Id)));

        private static void ValidateStorageProperty(Property maybeProperty, Guid propertyId)
        {
            if (maybeProperty is null)
                throw new NotFoundPropertyException(propertyId);
        }

        private static void ValidatePropertyNotNull(Property property)
        {
            if (property is null)
                throw new NullPropertyException();
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
            var invalidPropertyException = new InvalidPropertyException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                    invalidPropertyException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
            }

            invalidPropertyException.ThrowIfContainsErrors();
        }
    }
}