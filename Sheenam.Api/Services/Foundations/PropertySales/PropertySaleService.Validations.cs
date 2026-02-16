//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.PropertySales;
using Sheenam.Api.Models.Foundations.PropertySales.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.PropertySales
{
    public partial class PropertySaleService
    {
        private void ValidatePropertySaleOnAdd(PropertySale propertySale)
        {
            ValidatePropertySaleIsNotNull(propertySale);

            Validate(
                (Rule: IsInvalid(propertySale.Id), Parameter: nameof(PropertySale.Id)),
                (Rule: IsInvalid(propertySale.HostId), Parameter: nameof(PropertySale.HostId)),
                (Rule: IsInvalid(propertySale.Address), Parameter: nameof(PropertySale.Address)),
                (Rule: IsInvalid(propertySale.Description), Parameter: nameof(PropertySale.Description)),
                (Rule: IsInvalid(propertySale.Type), Parameter: nameof(PropertySale.Type)),
                (Rule: IsInvalid(propertySale.Status), Parameter: nameof(PropertySale.Status)),
                (Rule: IsInvalid(propertySale.SalePrice), Parameter: nameof(PropertySale.SalePrice)),
                (Rule: IsInvalid(propertySale.Area), Parameter: nameof(PropertySale.Area)),
                (Rule: IsInvalid(propertySale.CreatedDate), Parameter: nameof(PropertySale.CreatedDate)),
                (Rule: IsInvalid(propertySale.UpdatedDate), Parameter: nameof(PropertySale.UpdatedDate)),

                (Rule: IsNotSame(
                    firstDate: propertySale.UpdatedDate,
                    secondDate: propertySale.CreatedDate,
                    secondDateName: nameof(PropertySale.CreatedDate)),
                Parameter: nameof(PropertySale.UpdatedDate))
            );
        }

        private void ValidatePropertySaleOnModify(PropertySale propertySale)
        {
            ValidatePropertySaleIsNotNull(propertySale);

            Validate(
                (Rule: IsInvalid(propertySale.Id), Parameter: nameof(PropertySale.Id)),
                (Rule: IsInvalid(propertySale.HostId), Parameter: nameof(PropertySale.HostId)),
                (Rule: IsInvalid(propertySale.Address), Parameter: nameof(PropertySale.Address)),
                (Rule: IsInvalid(propertySale.Description), Parameter: nameof(PropertySale.Description)),
                (Rule: IsInvalid(propertySale.Type), Parameter: nameof(PropertySale.Type)),
                (Rule: IsInvalid(propertySale.Status), Parameter: nameof(PropertySale.Status)),
                (Rule: IsInvalid(propertySale.SalePrice), Parameter: nameof(PropertySale.SalePrice)),
                (Rule: IsInvalid(propertySale.Area), Parameter: nameof(PropertySale.Area)),
                (Rule: IsInvalid(propertySale.UpdatedDate), Parameter: nameof(PropertySale.UpdatedDate)),

                (Rule: IsSame(
                    firstDate: propertySale.UpdatedDate,
                    secondDate: propertySale.CreatedDate,
                    secondDateName: nameof(PropertySale.CreatedDate)),
                Parameter: nameof(PropertySale.UpdatedDate))
            );
        }

        public void ValidatePropertySaleId(Guid propertySaleId) =>
            Validate((Rule: IsInvalid(propertySaleId), Parameter: nameof(PropertySale.Id)));

        private static void ValidateStoragePropertySale(PropertySale maybePropertySale, Guid propertySaleId)
        {
            if (maybePropertySale is null)
            {
                throw new NotFoundPropertySaleException(propertySaleId);
            }
        }

        private void ValidatePropertySaleIsNotNull(PropertySale propertySale)
        {
            if (propertySale is null)
            {
                throw new NullPropertySaleException();
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

        private static dynamic IsInvalid(decimal price) => new
        {
            Condition = price <= 0,
            Message = "Price is required"
        };

        private static dynamic IsInvalid(double area) => new
        {
            Condition = area <= 0,
            Message = "Area is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsInvalid<T>(T value) where T : Enum => new
        {
            Condition = Enum.IsDefined(typeof(T), value) is false,
            Message = "Value is invalid"
        };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private static dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidPropertySaleException = new InvalidPropertySaleException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidPropertySaleException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidPropertySaleException.ThrowIfContainsErrors();
        }
    }
}