//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.PropertyViews;
using Sheenam.Api.Models.Foundations.PropertyViews.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.PropertyViews
{
    public partial class PropertyViewService
    {
        private void ValidatePropertyViewOnAdd(PropertyView propertyView)
        {
            ValidatePropertyViewNotNull(propertyView);

            Validate(
                (Rule: IsInvalid(propertyView.Id), Parameter: nameof(PropertyView.Id)),
                (Rule: IsInvalid(propertyView.ViewedDate), Parameter: nameof(PropertyView.ViewedDate)));
        }

        private static void ValidatePropertyViewId(Guid propertyViewId) =>
            Validate((Rule: IsInvalid(propertyViewId), Parameter: nameof(PropertyView.Id)));

        private static void ValidateStoragePropertyView(
            PropertyView maybePropertyView, Guid propertyViewId)
        {
            if (maybePropertyView is null)
                throw new NotFoundPropertyViewException(propertyViewId);
        }

        private static void ValidatePropertyViewNotNull(PropertyView propertyView)
        {
            if (propertyView is null)
                throw new NullPropertyViewException();
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
            var invalidPropertyViewException = new InvalidPropertyViewException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                    invalidPropertyViewException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
            }

            invalidPropertyViewException.ThrowIfContainsErrors();
        }
    }
}
