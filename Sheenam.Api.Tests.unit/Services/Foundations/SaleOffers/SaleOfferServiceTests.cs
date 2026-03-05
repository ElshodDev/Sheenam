//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.SaleOffers;
using Sheenam.Api.Services.Foundations.SaleOffers;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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

        private static SaleOffer CreateRandomSaleOffer() =>
            CreateSaleOfferFiller().Create();

        private static IQueryable<SaleOffer> CreateRandomSaleOffers() =>
            CreateSaleOfferFiller().Create(count: GetRandomNumber()).AsQueryable();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        // FIX: FormatterServices.GetUninitializedObject o'rniga
        // RuntimeHelpers.GetUninitializedObject ishlatiladi (.NET 9 uchun to'g'ri)
        private static SqlException GetSqlError() =>
            (SqlException)RuntimeHelpers.GetUninitializedObject(typeof(SqlException));

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static Filler<SaleOffer> CreateSaleOfferFiller()
        {
            var filler = new Filler<SaleOffer>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset())
                .OnType<DateTimeOffset?>().Use((DateTimeOffset?)GetRandomDateTimeOffset())
                .OnProperty(s => s.PropertySale).IgnoreIt()
                .OnProperty(s => s.Buyer).IgnoreIt();

            return filler;
        }
    }
}