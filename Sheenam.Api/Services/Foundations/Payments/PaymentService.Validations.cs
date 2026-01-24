//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Payments;
using Sheenam.Api.Models.Foundations.Payments.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.Payments
{
    public partial class PaymentService
    {
        private void ValidatePaymentOnAdd(Payment payment)
        {
            ValidatePaymentIsNotNull(payment);

            Validate(
                (Rule: IsInvalid(payment.Id), Parameter: nameof(Payment.Id)),
                (Rule: IsInvalid(payment.UserId), Parameter: nameof(Payment.UserId)),
                (Rule: IsInvalid(payment.Amount), Parameter: nameof(Payment.Amount)),
                (Rule: IsInvalid(payment.CreatedDate), Parameter: nameof(Payment.CreatedDate)),
                (Rule: IsInvalid(payment.UpdatedDate), Parameter: nameof(Payment.UpdatedDate)),
                (Rule: IsNotSame(
                    firstDate: payment.UpdatedDate,
                    secondDate: payment.CreatedDate,
                    secondDateName: nameof(Payment.CreatedDate)),
                Parameter: nameof(Payment.UpdatedDate))
            );
        }

        private static void ValidatePaymentIsNotNull(Payment payment)
        {
            if (payment is null)
            {
                throw new NullPaymentException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(decimal amount) => new
        {
            Condition = amount <= 0,
            Message = "Amount must be greater than zero"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidPaymentException = new InvalidPaymentException();

            foreach (var (rule, parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidPaymentException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidPaymentException.ThrowIfContainsErrors();
        }
    }
}