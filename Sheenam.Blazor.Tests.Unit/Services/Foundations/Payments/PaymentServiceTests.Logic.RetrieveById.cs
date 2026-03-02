//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Blazor.Models.Foundations.Payments;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Payments
{
    public partial class PaymentServiceTests
    {
        [Fact]
        public async Task ShouldRetrievePaymentByIdAsync()
        {
            // given
            Guid randomPaymentId = Guid.NewGuid();
            Guid inputPaymentId = randomPaymentId;
            Payment randomPayment = CreateRandomPayment();
            Payment retrievedPayment = randomPayment;
            Payment expectedPayment = retrievedPayment.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetPaymentByIdAsync(inputPaymentId))
                    .ReturnsAsync(retrievedPayment);

            // when
            Payment actualPayment =
                await this.paymentService.RetrievePaymentByIdAsync(inputPaymentId);

            // then
            actualPayment.Should().BeEquivalentTo(expectedPayment);

            this.apiBrokerMock.Verify(broker =>
                broker.GetPaymentByIdAsync(inputPaymentId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}