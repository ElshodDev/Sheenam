//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.SaleOffers;
using Sheenam.Api.Services.Foundations.SaleOffers;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.SaleOffers
{
    public partial class SaleOfferServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;

        private readonly ISaleOfferService saleOfferService;
        public SaleOfferServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.saleOfferService = new SaleOfferService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static SaleOffer CreateRandomSaleOffer() =>
            CreateSaleOfferFiller(GetRandomDateTimeOffset()).Create();

        private static IQueryable<SaleOffer> CreateRandomSaleOffers()
        {
            int randomCount = GetRandomNumber();
            var saleOffersList = new List<SaleOffer>();

            for (int i = 0; i < randomCount; i++)
            {
                saleOffersList.Add(CreateRandomSaleOffer());
            }

            return saleOffersList.AsQueryable();
        }

        private static Filler<SaleOffer> CreateSaleOfferFiller(DateTimeOffset dates)
        {
            var filler = new Filler<SaleOffer>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)
                .OnType<DateTimeOffset?>().Use(dates)
                .OnType<string>().Use(new MnemonicString())
                .OnProperty(s => s.OfferPrice).Use(GetRandomDecimal())
                .OnProperty(s => s.Status).Use(SaleOfferStatus.Pending)
                .OnProperty(s => s.CreatedDate).Use(dates)
                .OnProperty(s => s.ResponseDate).IgnoreIt()
                .OnProperty(s => s.RejectionReason).IgnoreIt();

            return filler;
        }
        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static decimal GetRandomDecimal() =>
            new Random().Next(1000, 100000);

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 99).GetValue();
    }
}