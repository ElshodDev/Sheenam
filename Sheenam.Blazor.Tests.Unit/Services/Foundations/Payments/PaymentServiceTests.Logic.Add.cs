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
        public async Task ShouldAddPaymentAsync()
        {
            // given
            Payment randomPayment = CreateRandomPayment();
            Payment inputPayment = randomPayment;
            Payment retrievedPayment = inputPayment;
            Payment expectedPayment = retrievedPayment;

            this.apiBrokerMock.Setup(broker =>
                broker.PostPaymentAsync(inputPayment))
                    .ReturnsAsync(retrievedPayment);

            // when
            Payment actualPayment =
                await this.paymentService.AddPaymentAsync(inputPayment);

            // then
            actualPayment.Should().BeEquivalentTo(expectedPayment);

            this.apiBrokerMock.Verify(broker =>
                broker.PostPaymentAsync(inputPayment),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}