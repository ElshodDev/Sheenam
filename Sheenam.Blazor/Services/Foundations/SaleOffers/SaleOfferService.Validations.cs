//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.SaleOffers;
using Sheenam.Blazor.Models.Foundations.SaleOffers.Exceptions;
namespace Sheenam.Blazor.Services.Foundations.SaleOffers
{
    public partial class SaleOfferService
    {
        private static void ValidateSaleOfferOnAdd(SaleOffer saleOffer)
        {
            ValidateSaleOfferNotNull(saleOffer);
            Validate(
                (Rule: IsInvalid(saleOffer.Id), Parameter: nameof(SaleOffer.Id)),
                (Rule: IsInvalid(saleOffer.PropertySaleId), Parameter: nameof(SaleOffer.PropertySaleId)),
                (Rule: IsInvalid(saleOffer.BuyerId), Parameter: nameof(SaleOffer.BuyerId)),
                (Rule: IsInvalid(saleOffer.OfferPrice), Parameter: nameof(SaleOffer.OfferPrice)),
                (Rule: IsInvalid(saleOffer.CreatedDate), Parameter: nameof(SaleOffer.CreatedDate)));
        }

        private static void ValidateSaleOfferOnModify(SaleOffer saleOffer)
        {
            ValidateSaleOfferNotNull(saleOffer);
            Validate(
                (Rule: IsInvalid(saleOffer.Id), Parameter: nameof(SaleOffer.Id)),
                (Rule: IsInvalid(saleOffer.PropertySaleId), Parameter: nameof(SaleOffer.PropertySaleId)),
                (Rule: IsInvalid(saleOffer.BuyerId), Parameter: nameof(SaleOffer.BuyerId)),
                (Rule: IsInvalid(saleOffer.OfferPrice), Parameter: nameof(SaleOffer.OfferPrice)),
                (Rule: IsInvalid(saleOffer.CreatedDate), Parameter: nameof(SaleOffer.CreatedDate)));
        }

        private static void ValidateSaleOfferId(Guid saleOfferId) =>
            Validate((Rule: IsInvalid(saleOfferId), Parameter: nameof(SaleOffer.Id)));

        private static void ValidateSaleOfferNotNull(SaleOffer saleOffer)
        {
            if (saleOffer is null)
                throw new NullSaleOfferException();
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
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
            var invalidSaleOfferException = new InvalidSaleOfferException();
            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidSaleOfferException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }
            invalidSaleOfferException.ThrowIfContainsErrors();
        }
    }
}