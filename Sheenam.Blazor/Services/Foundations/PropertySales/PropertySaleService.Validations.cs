//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.PropertySales;
using Sheenam.Blazor.Models.Foundations.PropertySales.Exceptions;
namespace Sheenam.Blazor.Services.Foundations.PropertySales
{
    public partial class PropertySaleService
    {
        private static void ValidatePropertySaleOnAdd(PropertySale propertySale) =>
            ValidatePropertySaleFields(propertySale);

        private static void ValidatePropertySaleOnModify(PropertySale propertySale) =>
            ValidatePropertySaleFields(propertySale);

        private static void ValidatePropertySaleFields(PropertySale propertySale)
        {
            ValidatePropertySaleNotNull(propertySale);
            Validate(
                (Rule: IsInvalid(propertySale.Id), Parameter: nameof(PropertySale.Id)),
                (Rule: IsInvalid(propertySale.HostId), Parameter: nameof(PropertySale.HostId)),
                (Rule: IsInvalid(propertySale.Address), Parameter: nameof(PropertySale.Address)),
                (Rule: IsInvalid(propertySale.SalePrice), Parameter: nameof(PropertySale.SalePrice)),
                (Rule: IsInvalid(propertySale.ListedDate), Parameter: nameof(PropertySale.ListedDate)));
        }

        private static void ValidatePropertySaleId(Guid propertySaleId) =>
            Validate((Rule: IsInvalid(propertySaleId), Parameter: nameof(PropertySale.Id)));

        private static void ValidatePropertySaleNotNull(PropertySale propertySale)
        {
            if (propertySale is null)
                throw new NullPropertySaleException();
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

        private static dynamic IsInvalid(decimal amount) => new
        {
            Condition = amount <= 0,
            Message = "Amount must be greater than 0"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
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