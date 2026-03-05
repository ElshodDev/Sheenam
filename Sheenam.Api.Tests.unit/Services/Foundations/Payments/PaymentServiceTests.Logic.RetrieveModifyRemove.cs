//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Payments;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Payments
{
    public partial class PaymentServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllPayments()
        {
            // given
            IQueryable<Payment> randomPayments = CreateRandomPayments();
            IQueryable<Payment> storagePayments = randomPayments;
            IQueryable<Payment> expectedPayments = storagePayments.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllPayments())
                    .Returns(storagePayments);

            // when
            IQueryable<Payment> actualPayments =
                this.paymentService.RetrieveAllPayments();

            // then
            actualPayments.Should().BeEquivalentTo(expectedPayments);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllPayments(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrievePaymentByIdAsync()
        {
            // given
            Guid randomPaymentId = Guid.NewGuid();
            Payment randomPayment = CreateRandomPayment();
            Payment storagePayment = randomPayment;
            Payment expectedPayment = storagePayment.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPaymentByIdAsync(randomPaymentId))
                    .ReturnsAsync(storagePayment);

            // when
            Payment actualPayment =
                await this.paymentService.RetrievePaymentByIdAsync(randomPaymentId);

            // then
            actualPayment.Should().BeEquivalentTo(expectedPayment);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPaymentByIdAsync(randomPaymentId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyPaymentAsync()
        {
            // given
            DateTimeOffset createdDate = GetRandomDateTimeOffset();
            DateTimeOffset updatedDate = createdDate.AddDays(1);
            Payment randomPayment = CreateRandomPayment(createdDate);
            Payment inputPayment = randomPayment;
            inputPayment.UpdatedDate = updatedDate;
            Payment storagePayment = randomPayment.DeepClone();
            storagePayment.UpdatedDate = createdDate;
            Payment expectedPayment = inputPayment.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPaymentByIdAsync(inputPayment.Id))
                    .ReturnsAsync(storagePayment);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdatePaymentAsync(inputPayment))
                    .ReturnsAsync(inputPayment);

            // when
            Payment actualPayment =
                await this.paymentService.ModifyPaymentAsync(inputPayment);

            // then
            actualPayment.Should().BeEquivalentTo(expectedPayment);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPaymentByIdAsync(inputPayment.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdatePaymentAsync(inputPayment),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemovePaymentByIdAsync()
        {
            // given
            Guid randomPaymentId = Guid.NewGuid();
            Payment randomPayment = CreateRandomPayment();
            Payment storagePayment = randomPayment;
            Payment expectedPayment = storagePayment.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPaymentByIdAsync(randomPaymentId))
                    .ReturnsAsync(storagePayment);

            this.storageBrokerMock.Setup(broker =>
                broker.DeletePaymentAsync(storagePayment))
                    .ReturnsAsync(expectedPayment);

            // when
            Payment actualPayment =
                await this.paymentService.RemovePaymentByIdAsync(randomPaymentId);

            // then
            actualPayment.Should().BeEquivalentTo(expectedPayment);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPaymentByIdAsync(randomPaymentId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeletePaymentAsync(storagePayment),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        private static IQueryable<Payment> CreateRandomPayments()
        {
            int count = GetRandomNumber();
            var payments = new List<Payment>();

            for (int i = 0; i < count; i++)
                payments.Add(CreateRandomPayment());

            return payments.AsQueryable();
        }
    }
}
