//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using Sheenam.Blazor.Models.Foundations.Payments;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Payments
{
    public partial class PaymentServiceTests
    {
        [Fact]
        public async Task ShouldRemovePaymentAsync()
        {
            // given
            Payment randomPayment = CreateRandomPayment();
            Guid inputPaymentId = randomPayment.Id;
            Payment retrievedPayment = randomPayment;
            Payment expectedPayment = retrievedPayment;

            this.apiBrokerMock.Setup(broker =>
                broker.DeletePaymentByIdAsync(inputPaymentId))
                    .ReturnsAsync(retrievedPayment);

            // when
            Payment actualPayment =
                await this.paymentService.RemovePaymentByIdAsync(inputPaymentId);

            // then
            actualPayment.Should().BeEquivalentTo(expectedPayment);

            this.apiBrokerMock.Verify(broker =>
                broker.DeletePaymentByIdAsync(inputPaymentId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}