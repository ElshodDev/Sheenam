//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using Sheenam.Api.Models.Foundations.Payments;
using Sheenam.Api.Models.Foundations.Payments.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Payments
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

            PaymentValidationException actualPaymentValidationException =
                await Assert.ThrowsAsync<PaymentValidationException>(
                    addPaymentTask.AsTask);

            // then
            actualPaymentValidationException.Should()
                .BeEquivalentTo(expectedPaymentValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPaymentValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPaymentAsync(It.IsAny<Payment>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public async Task ShouldThrowValidationExceptionOnAddIfPaymentIsInvalidAndLogItAsync(
     decimal invalidAmount)
        {
            // given
            var invalidPayment = new Payment
            {
                Id = Guid.Empty,
                UserId = Guid.Empty,
                Amount = invalidAmount,
                Method = (PaymentMethod)(-1),
                Status = (PaymentStatus)(-1),
                CreatedDate = default,
                UpdatedDate = default
            };

            var invalidPaymentException = new InvalidPaymentException();

            invalidPaymentException.UpsertDataList(
                key: nameof(Payment.Id),
                value: "Id is required");

            invalidPaymentException.UpsertDataList(
                key: nameof(Payment.UserId),
                value: "Id is required");

            invalidPaymentException.UpsertDataList(
                key: nameof(Payment.Amount),
                value: "Amount is required");

            invalidPaymentException.UpsertDataList(
                key: nameof(Payment.Method),
                value: "Value is not recognized");

            invalidPaymentException.UpsertDataList(
                key: nameof(Payment.Status),
                value: "Value is not recognized");

            invalidPaymentException.UpsertDataList(
                key: nameof(Payment.CreatedDate),
                value: "Date is required");

            invalidPaymentException.UpsertDataList(
                key: nameof(Payment.UpdatedDate),
                value: "Date is required");

            var expectedPaymentValidationException =
                new PaymentValidationException(invalidPaymentException);

            // when
            ValueTask<Payment> addPaymentTask =
                this.paymentService.AddPaymentAsync(invalidPayment);

            PaymentValidationException actualPaymentValidationException =
                await Assert.ThrowsAsync<PaymentValidationException>(
                    addPaymentTask.AsTask);

            // then
            actualPaymentValidationException.Should()
                .BeEquivalentTo(expectedPaymentValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPaymentValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPaymentAsync(It.IsAny<Payment>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}