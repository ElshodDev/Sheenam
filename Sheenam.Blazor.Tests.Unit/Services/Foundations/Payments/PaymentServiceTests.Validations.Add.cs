//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Blazor.Models.Foundations.Payments;
using Sheenam.Blazor.Models.Foundations.Payments.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Payments
{
    public partial class PaymentServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPaymentIsNullAndLogItAsync()
        {
            // given
            Payment nullPayment = null;
            var nullPaymentException = new NullPaymentException();

            var expectedPaymentValidationException =
                new PaymentValidationException(nullPaymentException);

            // when
            ValueTask<Payment> addPaymentTask =
                this.paymentService.AddPaymentAsync(nullPayment);

            // then
            await Assert.ThrowsAsync<PaymentValidationException>(() =>
                addPaymentTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPaymentValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostPaymentAsync(It.IsAny<Payment>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPaymentIsInvalidAndLogItAsync()
        {
            // given
            var invalidPayment = new Payment
            {
                Id = Guid.Empty,
                UserId = Guid.Empty,
                Amount = 0,
                PaymentDate = default
            };

            var invalidPaymentException = new InvalidPaymentException();

            invalidPaymentException.AddData(
                key: nameof(Payment.Id),
                values: "Id is required");

            invalidPaymentException.AddData(
                key: nameof(Payment.UserId),
                values: "Id is required");

            invalidPaymentException.AddData(
                key: nameof(Payment.Amount),
                values: "Amount must be greater than 0");

            invalidPaymentException.AddData(
                key: nameof(Payment.PaymentDate),
                values: "Date is required");

            var expectedPaymentValidationException =
                new PaymentValidationException(invalidPaymentException);

            // when
            ValueTask<Payment> addPaymentTask =
                this.paymentService.AddPaymentAsync(invalidPayment);

            // then
            await Assert.ThrowsAsync<PaymentValidationException>(() =>
                addPaymentTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPaymentValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostPaymentAsync(It.IsAny<Payment>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}