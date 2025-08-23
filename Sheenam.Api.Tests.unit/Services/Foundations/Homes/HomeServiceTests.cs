//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Services.Foundations.Homes;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;

namespace Sheenam.Api.Tests.unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IHomeService homeService;

        public HomeServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.homeService =
                new HomeService(storageBrokerMock.Object);
        }

        private static Home CreateRandomHome() =>
            CreateHomeFiller().Create();

        private static Filler<Home> CreateHomeFiller()
        {
            var filler = new Filler<Home>();

            filler.Setup()
            .OnType<Guid>().Use(Guid.NewGuid)
            .OnType<string>().Use(new MnemonicString())
            .OnProperty(h => h.NumberOfBedrooms).Use(new IntRange(1, 10))
            .OnProperty(h => h.NumberOfBathrooms).Use(new IntRange(1, 5))
            .OnProperty(h => h.Area).Use(new DoubleRange(20, 500))
            .OnProperty(h => h.Price).Use(() =>
             Convert.ToDecimal(new DoubleRange(100, 5000).GetValue()))
            .OnProperty(h => h.Type).Use(GetRandomHouseType)
            .OnProperty(h => h.IsVacant).Use(GetRandomBool)
            .OnProperty(h => h.IsPetAllowed).Use(GetRandomBool)
            .OnProperty(h => h.IsShared).Use(GetRandomBool);

            return filler;
        }
        private static HouseType GetRandomHouseType()
        {
            Array values = Enum.GetValues(typeof(HouseType));
            return (HouseType)values.GetValue(new Random().Next(values.Length));
        }

        private static bool GetRandomBool() =>
            new Random().Next(0, 2) == 0;
    }
}
