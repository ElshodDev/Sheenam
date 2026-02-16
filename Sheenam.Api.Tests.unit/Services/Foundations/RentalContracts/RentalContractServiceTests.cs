//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Brokers.DateTimes;
using Sheenam.Api.Brokers.Guids;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.RentalContracts;
using Sheenam.Api.Services.Foundations.RentalContracts;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.RentalContracts
{
    public partial class RentalContractServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<IGuidBroker> guidBrokerMock;
        private readonly IRentalContractService rentalContractService;

        public RentalContractServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.guidBrokerMock = new Mock<IGuidBroker>();

            this.rentalContractService = new RentalContractService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                guidBroker: this.guidBrokerMock.Object
                );
        }

        private static RentalContract CreateRandomRentalContract() =>
          CreateRentalContractFiller(date: GetRandomDateTimeOffset()).Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static SqlException GetSqlError() =>
          (SqlException)FormatterServices.GetSafeUninitializedObject(typeof(SqlException));


        private static T GetInvalidEnum<T>()
        {
            int randomNumber = GetRandomNumber();

            while (Enum.IsDefined(typeof(T), randomNumber) is true)
            {
                randomNumber = GetRandomNumber();
            }

            return (T)(object)randomNumber;
        }

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static Filler<RentalContract> CreateRentalContractFiller(DateTimeOffset date)
        {
            var filler = new Filler<RentalContract>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date);

            return filler;
        }
    }
}
