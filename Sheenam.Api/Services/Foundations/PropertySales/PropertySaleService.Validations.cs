//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.PropertySales;
using Sheenam.Api.Models.Foundations.PropertySales.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.PropertySales
{
    public partial class PropertySaleService
    {
        private void ValidatePropertySaleOnAdd(PropertySale propertySale)
        {
            ValidatePropertySaleNotNull(propertySale);

            Validate(
                (Rule: IsInvalid(propertySale.Id), Parameter: nameof(PropertySale.Id)),
                (Rule: IsInvalid(propertySale.HostId), Parameter: nameof(PropertySale.HostId)),
                (Rule: IsInvalid(propertySale.Address), Parameter: nameof(PropertySale.Address)),
                (Rule: IsInvalid(propertySale.Description), Parameter: nameof(PropertySale.Description)),
                (Rule: IsInvalid(propertySale.Type), Parameter: nameof(PropertySale.Type)),
                (Rule: IsInvalid(propertySale.SalePrice), Parameter: nameof(PropertySale.SalePrice)),
                (Rule: IsInvalid(propertySale.ListedDate), Parameter: nameof(PropertySale.ListedDate)),
                (Rule: IsInvalid(propertySale.CreatedDate), Parameter: nameof(PropertySale.CreatedDate)),
                (Rule: IsInvalid(propertySale.UpdatedDate), Parameter: nameof(PropertySale.UpdatedDate)));
        }

        private void ValidatePropertySaleNotNull(PropertySale propertySale)
        {
            if (propertySale is null)
            {
                throw new NullPropertySaleException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is Required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is Required"
        };

        private static dynamic IsInvalid(decimal price) => new
        {
            Condition = price <= 0,
            Message = "Price must be greater than zero"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is Required"
        };

        private static dynamic IsInvalid(HouseType houseType) => new
        {
            Condition = Enum.IsDefined(typeof(HouseType), houseType) is false,
            Message = "Value is invalid"
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