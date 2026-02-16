//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
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
            ValidatePaymentNotNull(payment);

            Validate(
                (Rule: IsInvalid(payment.Id), Parameter: nameof(Payment.Id)),
                (Rule: IsInvalid(payment.UserId), Parameter: nameof(Payment.UserId)),
                (Rule: IsInvalid(payment.Amount), Parameter: nameof(Payment.Amount)),
                (Rule: IsInvalid(payment.Method), Parameter: nameof(Payment.Method)),
                (Rule: IsInvalid(payment.Status), Parameter: nameof(Payment.Status)),
                (Rule: IsInvalid(payment.PaymentDate), Parameter: nameof(Payment.PaymentDate)),
                (Rule: IsInvalid(payment.TransactionReference), Parameter: nameof(Payment.TransactionReference)),
                (Rule: IsInvalid(payment.CreatedDate), Parameter: nameof(Payment.CreatedDate)),
                (Rule: IsInvalid(payment.UpdatedDate), Parameter: nameof(Payment.UpdatedDate)),

                (Rule: IsNotSame(
                    firstDate: payment.CreatedDate,
                    secondDate: payment.UpdatedDate,
                    secondDateName: nameof(Payment.UpdatedDate)),
                    Parameter: nameof(Payment.CreatedDate)));
        }

        private void ValidatePaymentOnModify(Payment payment)
        {
            ValidatePaymentNotNull(payment);

            Validate(
                (Rule: IsInvalid(payment.Id), Parameter: nameof(Payment.Id)),
                (Rule: IsInvalid(payment.UserId), Parameter: nameof(Payment.UserId)),
                (Rule: IsInvalid(payment.Amount), Parameter: nameof(Payment.Amount)),
                (Rule: IsInvalid(payment.Method), Parameter: nameof(Payment.Method)),
                (Rule: IsInvalid(payment.Status), Parameter: nameof(Payment.Status)),
                (Rule: IsInvalid(payment.PaymentDate), Parameter: nameof(Payment.PaymentDate)),
                (Rule: IsInvalid(payment.CreatedDate), Parameter: nameof(Payment.CreatedDate)),
                (Rule: IsInvalid(payment.UpdatedDate), Parameter: nameof(Payment.UpdatedDate)),

                (Rule: IsSame(
                    firstDate: payment.UpdatedDate,
                    secondDate: payment.CreatedDate,
                    secondDateName: nameof(Payment.CreatedDate)),
                    Parameter: nameof(Payment.UpdatedDate)));
        }

        private static void ValidateAgainstStoragePaymentOnModify(Payment inputPayment, Payment storagePayment)
        {
            ValidateStoragePayment(storagePayment, inputPayment.Id);

            Validate(
                (Rule: IsNotSame(
                    firstDate: inputPayment.CreatedDate,
                    secondDate: storagePayment.CreatedDate,
                    secondDateName: nameof(Payment.CreatedDate)),
                    Parameter: nameof(Payment.CreatedDate)),

                (Rule: IsSame(
                    firstDate: inputPayment.UpdatedDate,
                    secondDate: storagePayment.UpdatedDate,
                    secondDateName: nameof(Payment.UpdatedDate)),
                    Parameter: nameof(Payment.UpdatedDate)));
        }

        private static void ValidateStoragePayment(Payment maybePayment, Guid paymentId)
        {
            if (maybePayment is null)
            {
                throw new NotFoundPaymentException(paymentId);
            }
        }

        private void ValidatePaymentId(Guid paymentId) =>
             Validate((Rule: IsInvalid(paymentId), Parameter: nameof(Payment.Id)));

        private void ValidatePaymentNotNull(Payment payment)
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

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(decimal amount) => new
        {
            Condition = amount <= 0,
            Message = "Amount is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsInvalid<T>(T value) => new
        {
            Condition = IsEnumInvalid(value),
            Message = "Value is not recognized"
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

        private static bool IsEnumInvalid<T>(T value)
        {
            bool isDefined = Enum.IsDefined(typeof(T), value);
            return isDefined is false;
        }

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