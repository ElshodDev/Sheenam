//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;
using System.Linq.Expressions;
using Xeptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfHomeIsNullAndLogItAsync()
        {
            //given
            Home nullHome = null;
            var nullHomeException = new NullHomeException();

            var expectedHomeValidationException =
                new HomeValidationException(nullHomeException);

            //when
            ValueTask<Home> addHomeTask =
                 this.homeService.AddHomeAsync(nullHome);

            //then
            await Assert.ThrowsAsync<HomeValidationException>(() =>
            addHomeTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
          broker.LogError(It.Is(SameExceptionAs(
          expectedHomeValidationException))),
          Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfHomeIsInvalidAndLogAsync(
            string invalidText)
        {
            //given
            var invalidHome = new Home
            {
                Address = invalidText,
                AdditionalInfo = invalidText,
                NumberOfBathrooms = 0,
                NumberOfBedrooms = 0,
                Area = 0,
                Price = 0
            };


            var invalidHomeException = new InvalidHomeException();

            invalidHomeException.AddData(
                nameof(Home.Id),
                values: "Id is Required");

            invalidHomeException.AddData(
                nameof(Home.HostId),
                values: "Host Id is Required");

            invalidHomeException.AddData(
               nameof(Home.Address),
               values: "Text is Required");

            invalidHomeException.AddData(
               nameof(Home.AdditionalInfo),
               values: "Text is Required");

            invalidHomeException.AddData(
               nameof(Home.NumberOfBathrooms),
               values: "Number of bathrooms must be greater than 0");

            invalidHomeException.AddData(
               nameof(Home.NumberOfBedrooms),
               values: "Number of bedrooms must be greater than 0");

            invalidHomeException.AddData(
               nameof(Home.Area),
               values: "Area must be greater than 0");

            invalidHomeException.AddData(
              nameof(Home.Price),
              values: "Price must be greater than 0");

            var expectedHomeValidationException =
                new HomeValidationException(invalidHomeException);

            //when 
            ValueTask<Home> addHomeTask =
                this.homeService.AddHomeAsync(invalidHome);

            //then
            await Assert.ThrowsAsync<HomeValidationException>(() =>
            addHomeTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedHomeValidationException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertHomeAsync(It.IsAny<Home>()),
            Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        private Expression<Func<Exception, bool>> SameExceptionAs(Xeption expectedHomeException) =>
            actualException => actualException.SameExceptionAs(expectedHomeException);
    }
}
