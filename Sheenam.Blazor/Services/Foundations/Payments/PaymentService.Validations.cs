//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Payments;
using Sheenam.Blazor.Models.Foundations.Payments.Exceptions;

namespace Sheenam.Blazor.Services.Foundations.Payments
{
    public partial class PaymentService
    {
        private static void ValidatePaymentOnAdd(Payment payment) =>
            ValidatePaymentFields(payment);

        private static void ValidatePaymentOnModify(Payment payment) =>
            ValidatePaymentFields(payment);

        private static void ValidatePaymentFields(Payment payment)
        {
            ValidatePaymentNotNull(payment);

            Validate(
                (Rule: IsInvalid(payment.Id), Parameter: nameof(Payment.Id)),
                (Rule: IsInvalid(payment.UserId), Parameter: nameof(Payment.UserId)),
                (Rule: IsInvalid(payment.Amount), Parameter: nameof(Payment.Amount)),
                (Rule: IsInvalid(payment.PaymentDate), Parameter: nameof(Payment.PaymentDate)));
        }

        private static void ValidatePaymentId(Guid paymentId) =>
            Validate((Rule: IsInvalid(paymentId), Parameter: nameof(Payment.Id)));

        private static void ValidatePaymentNotNull(Payment payment)
        {
            if (payment is null)
                throw new NullPaymentException();
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
            var invalidPaymentException = new InvalidPaymentException();

            foreach ((dynamic rule, string parameter) in validations)
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