//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.PropertySales;
using Sheenam.Api.Services.Foundations.PropertySales;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using Xeptions;
using ObjectFillerDoubleRange = Tynamix.ObjectFiller.DoubleRange;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.PropertySales
{
    public partial class PropertySaleServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IPropertySaleService propertySaleService;

        public PropertySaleServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.propertySaleService = new PropertySaleService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static PropertySale CreateRandomPropertySale() =>
            CreatePropertySaleFiller(date: GetRandomDateTimeOffset()).Create();

        private static PropertySale CreateRandomPropertySale(DateTimeOffset date) =>
            CreatePropertySaleFiller(date).Create();

        private static IQueryable<PropertySale> CreateRandomPropertySales()
        {
            return CreatePropertySaleFiller(date: GetRandomDateTimeOffset())
                .Create(count: GetRandomNumber())
                .AsQueryable();
        }

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static SqlException GetSqlError() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static T GetInvalidEnum<T>() where T : struct, Enum
        {
            int randomNumber = GetRandomNumber();

            while (Enum.IsDefined((T)(object)randomNumber))
            {
                randomNumber = GetRandomNumber();
            }

            return (T)(object)randomNumber;
        }

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static Filler<PropertySale> CreatePropertySaleFiller(DateTimeOffset date)
        {
            var filler = new Filler<PropertySale>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date)
                .OnType<DateTimeOffset?>().Use(date)
                .OnType<int>().Use(GetRandomNumber())
                .OnType<int?>().Use(GetRandomNumber())
                .OnType<double>().Use(GetRandomPositiveDouble())
                .OnType<double?>().Use(GetRandomPositiveDouble())
                .OnType<decimal>().Use(GetRandomPositiveDecimal())
                .OnType<decimal?>().Use(GetRandomPositiveDecimal())

                .OnProperty(ps => ps.Host).IgnoreIt()

                .OnProperty(ps => ps.SalePrice).Use(GetRandomPositiveDecimal())
                .OnProperty(ps => ps.Area).Use(GetRandomPositiveDouble());

            return filler;
        }

        private static decimal GetRandomPositiveDecimal()
        {
            var random = new Random();
            return (decimal)random.NextDouble() * (1_000_000m - 1_000m) + 1_000m;
        }

        private static double GetRandomPositiveDouble() =>
          new ObjectFillerDoubleRange(minValue: 50, maxValue: 500).GetValue();
    }
}