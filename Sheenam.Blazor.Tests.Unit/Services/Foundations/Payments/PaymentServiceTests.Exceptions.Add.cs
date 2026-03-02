//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Payments;
using Sheenam.Blazor.Models.Foundations.Payments.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Payments
{
    public partial class PaymentServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            Payment somePayment = CreateRandomPayment();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(httpResponseMessage, "Bad request occurred");

            var invalidPaymentReferenceException =
                new InvalidPaymentReferenceException(httpResponseBadRequestException);

            var expectedPaymentDependencyValidationException =
                new PaymentDependencyValidationException(invalidPaymentReferenceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostPaymentAsync(It.IsAny<Payment>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<Payment> addPaymentTask =
                this.paymentService.AddPaymentAsync(somePayment);

            PaymentDependencyValidationException actualException =
                await Assert.ThrowsAsync<PaymentDependencyValidationException>(
                    addPaymentTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(
                expectedPaymentDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostPaymentAsync(It.IsAny<Payment>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPaymentDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Payment somePayment = CreateRandomPayment();
            var serviceException = new Exception();

            var failedPaymentServiceException =
                new FailedPaymentServiceException(serviceException);

            var expectedPaymentServiceException =
                new PaymentServiceException(failedPaymentServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostPaymentAsync(It.IsAny<Payment>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Payment> addPaymentTask =
                this.paymentService.AddPaymentAsync(somePayment);

            PaymentServiceException actualException =
                await Assert.ThrowsAsync<PaymentServiceException>(
                    addPaymentTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedPaymentServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostPaymentAsync(It.IsAny<Payment>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPaymentServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}