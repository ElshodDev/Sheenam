//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Payments;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Payments
{
    public partial class PaymentServiceTests
    {
        [Fact]
        public async Task ShouldAddPaymentAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTimeOffset();
            Payment randomPayment = CreateRandomPayment(randomDateTime);
            Payment inputPayment = randomPayment;
            Payment storagePayment = inputPayment;
            Payment expectedPayment = storagePayment.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPaymentAsync(inputPayment))
                    .ReturnsAsync(storagePayment);

            // when
            Payment actualPayment =
                await this.paymentService.AddPaymentAsync(inputPayment);

            // then
            actualPayment.Should().BeEquivalentTo(expectedPayment);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPaymentAsync(inputPayment),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}