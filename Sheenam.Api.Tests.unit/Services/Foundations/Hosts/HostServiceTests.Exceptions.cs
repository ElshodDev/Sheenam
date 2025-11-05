//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;
using Sheenam.Api.Models.Foundations.Hosts;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogitAsync()
        {
            // given
            Host someHost = CreateRandomHost();
            SqlException sqlException = GetSqlError();

            var failedHostStorageException =
                new FailedHostStorageException(sqlException);

            var expectedHostDependecyException =
                new HostDependencyException(failedHostStorageException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertHostAsync(someHost))
                .ThrowsAsync(sqlException);

            // when
            ValueTask<Host> addHostTask =
                this.hostService.AddHostAsync(someHost);

            // then 
            await Assert.ThrowsAsync<HostDependencyException>(() =>
            addHostTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
            broker.InsertHostAsync(someHost),
            Times.Once);

            this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(
                expectedHostDependecyException))),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDublicateKeyErrorOccursAndlogitAsync()
        {
            // given
            Host someHost = CreateRandomHost();
            string someMessage = GetRandomString();
            var duplicateKeyException =
               new DuplicateKeyException(someMessage);

            var alreadyExistsHostException =
                new AlreadyExistsHostException(duplicateKeyException);

            var expectedHostDependencyValidationException =
                new HomeDependencyValidationException(alreadyExistsHostException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertHostAsync(someHost))
                .ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<Host> addHostTask =
                this.hostService.AddHostAsync(someHost);

            // then
            await Assert.ThrowsAsync<HomeDependencyValidationException>(() =>
               addHostTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
            broker.InsertHostAsync(someHost),
            Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostDependencyValidationException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
