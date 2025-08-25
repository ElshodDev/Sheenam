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

        private Expression<Func<Exception, bool>> SameExceptionAs(Xeption expectedHomeException) =>
            actualException => actualException.SameExceptionAs(expectedHomeException);
    }
}
