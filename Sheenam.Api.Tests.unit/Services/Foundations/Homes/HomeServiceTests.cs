//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Services.Foundations.Homes;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IHomeService homeService;

        public HomeServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.homeService = new HomeService(
                storageBrokerMock.Object,
                loggingBrokerMock.Object);
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static Home CreateRandomHome() =>
            CreateHomeFiller(GetRandomDateTimeOffset()).Create();

        private IQueryable<Home> CreateRandomHomes()
        {
            int randomCount = GetRandomNumber();
            var homesList = new List<Home>();

            for (int i = 0; i < randomCount; i++)
            {
                homesList.Add(CreateRandomHome());
            }

            return homesList.AsQueryable();
        }

        private static Filler<Home> CreateHomeFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Home>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)
                .OnType<string>().Use(new MnemonicString())

                // Asosiy xususiyatlar
                .OnProperty(h => h.NumberOfBedrooms).Use(new IntRange(1, 10))
                .OnProperty(h => h.NumberOfBathrooms).Use(new IntRange(1, 5))
                .OnProperty(h => h.Area).Use(new DoubleRange(20, 500))

                // Enumlar va Boollar
                .OnProperty(h => h.Type).Use(GetRandomHouseType())
                .OnProperty(h => h.ListingType).Use(GetRandomListingType()) // Random ListingType
                .OnProperty(h => h.IsVacant).Use(GetRandomBool())
                .OnProperty(h => h.IsPetAllowed).Use(GetRandomBool())
                .OnProperty(h => h.IsShared).Use(GetRandomBool())
                .OnProperty(h => h.IsFeatured).Use(GetRandomBool())

                // Yangi Narx va Sana propertylari
                .OnProperty(h => h.MonthlyRent).Use(GetRandomDecimal())
                .OnProperty(h => h.SalePrice).Use(GetRandomDecimal())
                .OnProperty(h => h.SecurityDeposit).Use(GetRandomDecimal())
                .OnProperty(h => h.ImageUrls).Use(GetRandomString())
                .OnProperty(h => h.ListedDate).Use(dates)
                .OnProperty(h => h.Price).Use(GetRandomDecimal());

            return filler;
        }

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static HouseType GetRandomHouseType()
        {
            Array values = Enum.GetValues(typeof(HouseType));
            return (HouseType)values.GetValue(new Random().Next(values.Length));
        }

        // Yangi qo'shilgan ListingType uchun random
        private static ListingType GetRandomListingType()
        {
            Array values = Enum.GetValues(typeof(ListingType));
            return (ListingType)values.GetValue(new Random().Next(values.Length));
        }

        private static bool GetRandomBool() =>
            new Random().Next(0, 2) == 0;

        private static decimal GetRandomDecimal() =>
            new Random().Next(100, 10000);

        private static int GetRandomNumber() =>
            new Random().Next(2, 10);

        private static string GetRandomString() =>
            new MnemonicString().GetValue();
    }
}