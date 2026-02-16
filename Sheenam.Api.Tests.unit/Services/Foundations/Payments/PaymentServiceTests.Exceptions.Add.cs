//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.Payments;
using Sheenam.Api.Models.Foundations.Payments.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Payments
{
    public partial class PaymentServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Payment somePayment = CreateRandomPayment();
            SqlException sqlException = GetSqlException();
            var failedPaymentStorageException = new FailedPaymentStorageException(sqlException);

            var expectedPaymentDependencyException =
                new PaymentDependencyException(failedPaymentStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPaymentAsync(It.IsAny<Payment>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Payment> addPaymentTask =
                this.paymentService.AddPaymentAsync(somePayment);

            PaymentDependencyException actualPaymentDependencyException =
                await Assert.ThrowsAsync<PaymentDependencyException>(
                    addPaymentTask.AsTask);

            // then
            actualPaymentDependencyException.Should()
                .BeEquivalentTo(expectedPaymentDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPaymentAsync(It.IsAny<Payment>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPaymentDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfPaymentAlreadyExistsAndLogItAsync()
        {
            // given
            Payment somePayment = CreateRandomPayment();
            string someMessage = GetRandomString();
            var duplicateKeyException = new DuplicateKeyException(someMessage);

            var alreadyExistsPaymentException =
                new AlreadyExistsPaymentException(duplicateKeyException);

            var expectedPaymentDependencyValidationException =
                new PaymentDependencyValidationException(alreadyExistsPaymentException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPaymentAsync(It.IsAny<Payment>()))
                    .ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<Payment> addPaymentTask =
                this.paymentService.AddPaymentAsync(somePayment);

            PaymentDependencyValidationException actualPaymentDependencyValidationException =
                await Assert.ThrowsAsync<PaymentDependencyValidationException>(
                    addPaymentTask.AsTask);

            // then
            actualPaymentDependencyValidationException.Should()
                .BeEquivalentTo(expectedPaymentDependencyValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPaymentAsync(It.IsAny<Payment>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPaymentDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}