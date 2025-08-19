//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogitAsync()
        {
            //given 
            Guest someGuest = CreateRandomGuest();
            SqlException sqlException = GetSqlError();

            var failedGuestStorageException = new
                FailedGuestStorageException(sqlException);

            var expectedGuestDependecyException = new
                GuestDependecyException(failedGuestStorageException);

            this.storageBrokerMock.Setup(broker =>
            broker.InserGuestAsync(someGuest))
                .ThrowsAsync(sqlException);


            //when 
            ValueTask<Guest> addGuestTask =
                this.guestService.AddGuestAsync(someGuest);


            //then
            await Assert.ThrowsAsync<GuestDependecyException>(() =>
            addGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
            broker.InserGuestAsync(someGuest),
            Times.Once);

            this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(
                expectedGuestDependecyException))),
                Times.Once);


            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDublicateKeyErrorOccursAndlogitAsync()
        {
            //given 
            Guest someGuest = CreateRandomGuest();
            string someMessage = GetRandomString();

            var duplicateKeyException = new
                DuplicateKeyException(someMessage);

            var alreadyExistGuestException =
                new AlreadyExistGuestException(duplicateKeyException);


            var expectedGuestDependencyValidationException =
                new GuestDependecyValidationException(alreadyExistGuestException);

            this.storageBrokerMock.Setup(broker =>
            broker.InserGuestAsync(someGuest))
                .ThrowsAsync(duplicateKeyException);

            //when 
            ValueTask<Guest> addGuestTask =
               this.guestService.AddGuestAsync(someGuest);

            //then
            await Assert.ThrowsAsync<GuestDependecyValidationException>(() =>
            addGuestTask.AsTask());


            this.storageBrokerMock.Verify(broker =>
            broker.InserGuestAsync(someGuest),
            Times.Once);


            this.loggingBrokerMock.Verify(Broker =>
            Broker.LogError(It.Is(SameExceptionAs(
                expectedGuestDependencyValidationException))),
                Times.Once);


            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            //given 
            Guest someGuest = CreateRandomGuest();
            string ErrorDetermine = string.Empty;

            var serviceException = new Exception();

            var failedGuestServiceException =
                new FailedGuestServiceException(serviceException);

            var expectedGuestServiceException =
                new GuestServiceException(failedGuestServiceException);

            this.storageBrokerMock.Setup(broker =>
            broker.InserGuestAsync(someGuest))
                .ThrowsAsync(serviceException);

            //when
            ValueTask<Guest> addGuestTask =
                this.guestService.AddGuestAsync(someGuest);

            //then
            await Assert.ThrowsAsync<GuestServiceException>(() =>
            addGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
            broker.InserGuestAsync(someGuest),
            Times.Once);


            this.loggingBrokerMock.Verify(Broker =>
            Broker.LogError(It.Is(SameExceptionAs(
                expectedGuestServiceException))),
                Times.Once);


            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
