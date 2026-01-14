//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.HomeRequests;
using Sheenam.Api.Services.Foundations.HomeRequests;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IHomeRequestService homeRequestService;

        public HomeRequestServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.homeRequestService = new HomeRequestService(
                 storageBroker: this.storageBrokerMock.Object,
                 loggingBroker: this.loggingBrokerMock.Object);
        }

        private static HomeRequest CreateRandomHomeRequest() =>
            CreateHomeRequestFiller(date: GetRandomDateTimeOffset()).Create();

        private static IQueryable<HomeRequest> CreateRandomHomeRequests()
        {
            int randomCount = new Random().Next(2, 10);
            var homerequests = new List<HomeRequest>();

            for (int i = 0; i < randomCount; i++)
            {
                homerequests.Add(CreateRandomHomeRequest());
            }
            return homerequests.AsQueryable();
        }

        private static Filler<HomeRequest> CreateHomeRequestFiller(DateTimeOffset date)
        {
            var filler = new Filler<HomeRequest>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date)
                // ✅ YANGI:  RejectionReason default null bo'lishi kerak
                .OnProperty(hr => hr.RejectionReason).Use(() => null)
                // ✅ YANGI: Status default Pending bo'lishi kerak
                .OnProperty(hr => hr.Status).Use(HomeRequestStatus.Pending);

            return filler;
        }

        // ✅ YANGI: Status bilan random HomeRequest yaratish uchun helper method
        private static HomeRequest CreateRandomHomeRequestWithStatus(HomeRequestStatus status)
        {
            HomeRequest homeRequest = CreateRandomHomeRequest();
            homeRequest.Status = status;
            return homeRequest;
        }

        private static string GetRandomString() =>
           new MnemonicString().GetValue();

        private static SqlException GetSqlException() =>
      (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
      actualException => actualException.SameExceptionAs(expectedException);

        private static DateTimeOffset GetRandomDateTimeOffset() =>
          new DateTimeRange(earliestDate: new DateTime()).GetValue();
    }
}