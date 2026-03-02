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
        public async Task ShouldRetrieveAllPaymentsAsync()
        {
            // given
            IQueryable<Payment> randomPayments = CreateRandomPayments();
            IQueryable<Payment> retrievedPayments = randomPayments;
            IQueryable<Payment> expectedPayments = retrievedPayments;

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllPaymentsAsync())
                    .ReturnsAsync(randomPayments.ToList());

            // when
            IQueryable<Payment> actualPayments =
                await this.paymentService.RetrieveAllPaymentsAsync();

            // then
            actualPayments.Should().BeEquivalentTo(expectedPayments);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllPaymentsAsync(),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}