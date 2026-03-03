//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Moq;
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.PropertySales;
using Sheenam.Blazor.Models.Foundations.PropertySales.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.PropertySales
{
    public partial class PropertySaleServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            PropertySale somePropertySale = CreateRandomPropertySale();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(httpResponseMessage, "Bad request occurred");

            var invalidPropertySaleReferenceException =
                new InvalidPropertySaleReferenceException(httpResponseBadRequestException);

            var expectedPropertySaleDependencyValidationException =
                new PropertySaleDependencyValidationException(invalidPropertySaleReferenceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostPropertySaleAsync(It.IsAny<PropertySale>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<PropertySale> addPropertySaleTask =
                this.propertySaleService.AddPropertySaleAsync(somePropertySale);

            PropertySaleDependencyValidationException actualException =
                await Assert.ThrowsAsync<PropertySaleDependencyValidationException>(
                    addPropertySaleTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(
                expectedPropertySaleDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostPropertySaleAsync(It.IsAny<PropertySale>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertySaleDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            PropertySale somePropertySale = CreateRandomPropertySale();
            var serviceException = new Exception();

            var failedPropertySaleServiceException =
                new FailedPropertySaleServiceException(serviceException);

            var expectedPropertySaleServiceException =
                new PropertySaleServiceException(failedPropertySaleServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostPropertySaleAsync(It.IsAny<PropertySale>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<PropertySale> addPropertySaleTask =
                this.propertySaleService.AddPropertySaleAsync(somePropertySale);

            PropertySaleServiceException actualException =
                await Assert.ThrowsAsync<PropertySaleServiceException>(
                    addPropertySaleTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedPropertySaleServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostPropertySaleAsync(It.IsAny<PropertySale>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertySaleServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}