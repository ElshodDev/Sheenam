//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Payments;
using Sheenam.Api.Services.Foundations.Payments;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Payments
{
    public partial class PaymentServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;

        private readonly IPaymentService paymentService;

        public PaymentServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.paymentService = new PaymentService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }
        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static Payment CreateRandomPayment() =>
            CreateRandomPayment(GetRandomDateTimeOffset());

        private static Payment CreateRandomPayment(DateTimeOffset dates)
        {
            var filler = new Filler<Payment>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)
                .OnProperty(p => p.Amount).Use(GetRandomDecimal())
                .OnProperty(p => p.Method).Use(PaymentMethod.Card)
                .OnProperty(p => p.Status).Use(PaymentStatus.Pending)
                .OnProperty(p => p.CreatedDate).Use(dates)
                .OnProperty(p => p.UpdatedDate).Use(dates);

            return filler.Create();
        }

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static decimal GetRandomDecimal() =>
            new Random().Next(100, 10000);

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 99).GetValue();
    }
}