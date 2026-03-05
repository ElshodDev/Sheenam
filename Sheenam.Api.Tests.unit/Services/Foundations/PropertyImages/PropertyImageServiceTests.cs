//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.PropertyImages;
using Sheenam.Api.Services.Foundations.PropertyImages;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.PropertyImages
{
    public partial class PropertyImageServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IPropertyImageService propertyImageService;

        public PropertyImageServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.propertyImageService = new PropertyImageService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static PropertyImage CreateRandomPropertyImage() =>
            CreatePropertyImageFiller().Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static SqlException GetSqlError() =>
            (SqlException)RuntimeHelpers.GetUninitializedObject(typeof(SqlException));

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static Filler<PropertyImage> CreatePropertyImageFiller()
        {
            var filler = new Filler<PropertyImage>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset())
                .OnProperty(pi => pi.Property).IgnoreIt();

            return filler;
        }
    }
}
