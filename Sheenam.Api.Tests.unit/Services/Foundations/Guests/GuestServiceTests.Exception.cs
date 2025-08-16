//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Moq;
using Npgsql;
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
            PostgresException postgresException = GetPostgresError();

            var failedGuestStorageException = new 
                FailedGuestStorageException(postgresException);

            var expectedGuestDependecyException = new 
                GuestDependecyException(failedGuestStorageException);

            this.storageBrokerMock.Setup(broker =>
            broker.InserGuestAsync(someGuest))
                .ThrowsAsync(postgresException);


            //when 
            ValueTask<Guest> addGuestTask=
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

            this.storageBrokerMock.Setup(broker=>
            broker.InserGuestAsync(someGuest))
                .ThrowsAsync(duplicateKeyException);

            //when 
            ValueTask<Guest> addGuestTask =
               this.guestService.AddGuestAsync(someGuest);

            //then
            await Assert.ThrowsAsync<GuestDependecyValidationException>(() =>
            addGuestTask.AsTask());


            this.storageBrokerMock.Verify(broker=>
            broker.InserGuestAsync(someGuest), 
            Times.Once);
            

            this.loggingBrokerMock.Verify(Broker=>
            Broker.LogError(It.Is(SameExceptionAs(
                expectedGuestDependencyValidationException))),
                Times.Once);


            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
