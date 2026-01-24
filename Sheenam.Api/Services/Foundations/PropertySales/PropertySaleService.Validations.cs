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
                (Rule: IsInvalid(propertySale.Type), Parameter: nameof(PropertySale.Type)),
                (Rule: IsInvalid(propertySale.Address), Parameter: nameof(PropertySale.Address)),
                (Rule: IsInvalid(propertySale.Description), Parameter: nameof(PropertySale.Description)),
                (Rule: IsInvalid(propertySale.NumberOfBedrooms), Parameter: nameof(PropertySale.NumberOfBedrooms)),
                (Rule: IsInvalid(propertySale.NumberOfBathrooms), Parameter: nameof(PropertySale.NumberOfBathrooms)),
                (Rule: IsInvalid(propertySale.Area), Parameter: nameof(PropertySale.Area)),
                (Rule: IsInvalid(propertySale.SalePrice), Parameter: nameof(PropertySale.SalePrice)),
                (Rule: IsInvalid(propertySale.ListedDate), Parameter: nameof(PropertySale.ListedDate)),
                (Rule: IsInvalid(propertySale.CreatedDate), Parameter: nameof(PropertySale.CreatedDate)),
                (Rule: IsInvalid(propertySale.UpdatedDate), Parameter: nameof(PropertySale.UpdatedDate))
            );
        }

        private void ValidatePropertySaleOnModify(PropertySale propertySale)
        {
            ValidatePropertySaleNotNull(propertySale);
            Validate(
                (Rule: IsInvalid(propertySale.Id), Parameter: nameof(PropertySale.Id)),
                (Rule: IsInvalid(propertySale.HostId), Parameter: nameof(PropertySale.HostId)),
                (Rule: IsInvalid(propertySale.Type), Parameter: nameof(PropertySale.Type)),
                (Rule: IsInvalid(propertySale.Address), Parameter: nameof(PropertySale.Address)),
                (Rule: IsInvalid(propertySale.Description), Parameter: nameof(PropertySale.Description)),
                (Rule: IsInvalid(propertySale.NumberOfBedrooms), Parameter: nameof(PropertySale.NumberOfBedrooms)),
                (Rule: IsInvalid(propertySale.NumberOfBathrooms), Parameter: nameof(PropertySale.NumberOfBathrooms)),
                (Rule: IsInvalid(propertySale.Area), Parameter: nameof(PropertySale.Area)),
                (Rule: IsInvalid(propertySale.SalePrice), Parameter: nameof(PropertySale.SalePrice)),
                (Rule: IsInvalid(propertySale.ListedDate), Parameter: nameof(PropertySale.ListedDate)),
                (Rule: IsInvalid(propertySale.CreatedDate), Parameter: nameof(PropertySale.CreatedDate)),
                (Rule: IsInvalid(propertySale.UpdatedDate), Parameter: nameof(PropertySale.UpdatedDate))
            );
        }

        private static void ValidateStoragePropertySale(PropertySale maybePropertySale, Guid propertySaleId)
        {
            if (maybePropertySale is null)
            {
                throw new NotFoundPropertySaleException(propertySaleId);
            }
        }

        private void ValidatePropertySaleId(Guid propertySaleId)
        {
            if (propertySaleId == Guid.Empty)
            {
                throw new InvalidPropertySaleException();
            }
        }

        private void ValidatePropertySaleNotNull(PropertySale propertySale)
        {
            if (propertySale is null)
            {
                throw new NullPropertySaleException();
            }
        }

        private static dynamic IsInvalid(HouseType type) => new
        {
            Condition = Enum.IsDefined(typeof(HouseType), type) is false,
            Message = "Value is invalid"
        };

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

        private static dynamic IsInvalid(int number) => new
        {
            Condition = number < 0,
            Message = "Number must be non-negative"
        };

        private static dynamic IsInvalid(double number) => new
        {
            Condition = number < 0,
            Message = "Number must be non-negative"
        };

        private static dynamic IsInvalid(decimal number) => new
        {
            Condition = number < 0,
            Message = "Number must be non-negative"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidPropertySaleException = new InvalidPropertySaleException();

            foreach (var (rule, parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidPropertySaleException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            if (invalidPropertySaleException.Data.Count > 0)
            {
                throw invalidPropertySaleException;
            }
        }
    }
}