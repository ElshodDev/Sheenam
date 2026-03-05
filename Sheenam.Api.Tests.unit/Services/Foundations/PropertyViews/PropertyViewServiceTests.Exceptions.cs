//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.PropertyViews;
using Sheenam.Api.Models.Foundations.PropertyViews.Exceptions;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.PropertyViews
{
    public partial class PropertyViewServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            PropertyView somePropertyView = CreateRandomPropertyView();
            SqlException sqlException = GetSqlError();

            var failedPropertyViewStorageException =
                new FailedPropertyViewStorageException(sqlException);

            var expectedPropertyViewDependencyException =
                new PropertyViewDependencyException(failedPropertyViewStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertyViewAsync(somePropertyView))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<PropertyView> addPropertyViewTask =
                this.propertyViewService.AddPropertyViewAsync(somePropertyView);

            PropertyViewDependencyException actualPropertyViewDependencyException =
                await Assert.ThrowsAsync<PropertyViewDependencyException>(() =>
                    addPropertyViewTask.AsTask());

            // then
            actualPropertyViewDependencyException.Should().BeEquivalentTo(
                expectedPropertyViewDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyViewAsync(somePropertyView),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPropertyViewDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfUnexpectedErrorOccursAndLogItAsync()
        {
            // given
            PropertyView somePropertyView = CreateRandomPropertyView();
            var serviceException = new Exception();

            var failedPropertyViewServiceException =
                new FailedPropertyViewServiceException(serviceException);

            var expectedPropertyViewServiceException =
                new PropertyViewServiceException(failedPropertyViewServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertyViewAsync(somePropertyView))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<PropertyView> addPropertyViewTask =
                this.propertyViewService.AddPropertyViewAsync(somePropertyView);

            PropertyViewServiceException actualPropertyViewServiceException =
                await Assert.ThrowsAsync<PropertyViewServiceException>(() =>
                    addPropertyViewTask.AsTask());

            // then
            actualPropertyViewServiceException.Should().BeEquivalentTo(
                expectedPropertyViewServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertyViewAsync(somePropertyView),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertyViewServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
