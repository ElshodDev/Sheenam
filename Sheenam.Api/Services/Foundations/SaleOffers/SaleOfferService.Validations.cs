//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.SaleOffers;
using Sheenam.Api.Models.Foundations.SaleOffers.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.SaleOffers
{
    public partial class SaleOfferService
    {
        private void ValidateSaleOfferOnAdd(SaleOffer saleOffer)
        {
            ValidateSaleOfferIsNotNull(saleOffer);

            Validate(
                (Rule: IsInvalid(saleOffer.Id), Parameter: nameof(SaleOffer.Id)),
                (Rule: IsInvalid(saleOffer.PropertySaleId), Parameter: nameof(SaleOffer.PropertySaleId)),
                (Rule: IsInvalid(saleOffer.BuyerId), Parameter: nameof(SaleOffer.BuyerId)),
                (Rule: IsInvalid(saleOffer.Message), Parameter: nameof(SaleOffer.Message)),
                (Rule: IsInvalid(saleOffer.OfferPrice), Parameter: nameof(SaleOffer.OfferPrice)),
                (Rule: IsInvalid(saleOffer.Status), Parameter: nameof(SaleOffer.Status)),
                (Rule: IsInvalid(saleOffer.CreatedDate), Parameter: nameof(SaleOffer.CreatedDate))
            );
        }

        private void ValidateSaleOfferOnModify(SaleOffer saleOffer)
        {
            ValidateSaleOfferIsNotNull(saleOffer);

            Validate(
                (Rule: IsInvalid(saleOffer.Id), Parameter: nameof(SaleOffer.Id)),
                (Rule: IsInvalid(saleOffer.PropertySaleId), Parameter: nameof(SaleOffer.PropertySaleId)),
                (Rule: IsInvalid(saleOffer.BuyerId), Parameter: nameof(SaleOffer.BuyerId)),
                (Rule: IsInvalid(saleOffer.Message), Parameter: nameof(SaleOffer.Message)),
                (Rule: IsInvalid(saleOffer.OfferPrice), Parameter: nameof(SaleOffer.OfferPrice)),
                (Rule: IsInvalid(saleOffer.Status), Parameter: nameof(SaleOffer.Status)),
                (Rule: IsInvalid(saleOffer.CreatedDate), Parameter: nameof(SaleOffer.CreatedDate))
            );
        }

        public void ValidateSaleOfferId(Guid saleOfferId) =>
            Validate((Rule: IsInvalid(saleOfferId), Parameter: nameof(SaleOffer.Id)));

        private static void ValidateStorageSaleOffer(SaleOffer maybeSaleOffer, Guid saleOfferId)
        {
            if (maybeSaleOffer is null)
            {
                throw new NotFoundSaleOfferException(saleOfferId);
            }
        }

        private void ValidateSaleOfferIsNotNull(SaleOffer saleOffer)
        {
            if (saleOffer is null)
            {
                throw new NullSaleOfferException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(decimal price) => new
        {
            Condition = price <= 0,
            Message = "Price must be greater than zero"
        };

        private static dynamic IsInvalid(SaleOfferStatus status) => new
        {
            Condition = Enum.IsDefined(typeof(SaleOfferStatus), status) is false,
            Message = "Value is invalid"
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