//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using Sheenam.Api.Models.Foundations.SaleOffers;
using Sheenam.Api.Models.Foundations.SaleOffers.Exceptions;
using System.Collections;

namespace Sheenam.Api.Tests.unit.Services.Foundations.SaleOffers
{
    public partial class SaleOfferServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfSaleOfferIsNullAndLogItAsync()
        {
            // given
            SaleOffer nullSaleOffer = null;

            var nullSaleOfferException = new NullSaleOfferException();

            var expectedSaleOfferValidationException =
                new SaleOfferValidationException(nullSaleOfferException);

            // when
            ValueTask<SaleOffer> addSaleOfferTask =
                this.saleOfferService.AddSaleOfferAsync(nullSaleOffer);

            SaleOfferValidationException actualSaleOfferValidationException =
                await Assert.ThrowsAsync<SaleOfferValidationException>(
                    addSaleOfferTask.AsTask);

            // then 
            Assert.NotNull(actualSaleOfferValidationException);
            Assert.Equal(expectedSaleOfferValidationException.Message,
                actualSaleOfferValidationException.Message);
            Assert.IsType<NullSaleOfferException>(
                actualSaleOfferValidationException.InnerException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleOfferValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSaleOfferAsync(It.IsAny<SaleOffer>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData((string)null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfSaleOfferIsInvalidAndLogItAsync(
            string invalidText)
        {
            var invalidSaleOffer = new SaleOffer
            {
                Id = Guid.Empty,
                PropertySaleId = Guid.Empty,
                BuyerId = Guid.Empty,
                Message = invalidText,
                OfferPrice = 0,
                Status = (SaleOfferStatus)999,
                CreatedDate = default
            };

            var invalidSaleOfferException = new InvalidSaleOfferException();

            invalidSaleOfferException.UpsertDataList(
                key: nameof(SaleOffer.Id),
                value: "Id is required");

            invalidSaleOfferException.UpsertDataList(
                key: nameof(SaleOffer.PropertySaleId),
                value: "Id is required");

            invalidSaleOfferException.UpsertDataList(
                key: nameof(SaleOffer.BuyerId),
                value: "Id is required");

            invalidSaleOfferException.UpsertDataList(
                key: nameof(SaleOffer.OfferPrice),
                value: "Price must be greater than zero");

            invalidSaleOfferException.UpsertDataList(
                key: nameof(SaleOffer.Status),
                value: "Value is invalid");

            invalidSaleOfferException.UpsertDataList(
                key: nameof(SaleOffer.CreatedDate),
                value: "Date is required");

            var expectedSaleOfferValidationException =
                new SaleOfferValidationException(invalidSaleOfferException);

            // when
            ValueTask<SaleOffer> addSaleOfferTask =
                this.saleOfferService.AddSaleOfferAsync(invalidSaleOffer);

            SaleOfferValidationException actualSaleOfferValidationException =
                await Assert.ThrowsAsync<SaleOfferValidationException>(
                    addSaleOfferTask.AsTask);

            // then 
            actualSaleOfferValidationException.InnerException.Data.Should()
                .BeEquivalentTo(expectedSaleOfferValidationException.InnerException.Data);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleOfferValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSaleOfferAsync(It.IsAny<SaleOffer>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfOfferPriceIsNegativeAndLogItAsync()
        {
            // given
            SaleOffer randomSaleOffer = CreateRandomSaleOffer();
            SaleOffer invalidSaleOffer = randomSaleOffer;
            invalidSaleOffer.OfferPrice = -100;

            var invalidSaleOfferException = new InvalidSaleOfferException();

            invalidSaleOfferException.UpsertDataList(
                key: nameof(SaleOffer.OfferPrice),
                value: "Price must be greater than zero");

            var expectedSaleOfferValidationException =
                new SaleOfferValidationException(invalidSaleOfferException);

            // when
            ValueTask<SaleOffer> addSaleOfferTask =
                this.saleOfferService.AddSaleOfferAsync(invalidSaleOffer);

            SaleOfferValidationException actualSaleOfferValidationException =
                await Assert.ThrowsAsync<SaleOfferValidationException>(
                    addSaleOfferTask.AsTask);
            // then
            var actualInvalidException = Assert.IsType<InvalidSaleOfferException>(
                actualSaleOfferValidationException.InnerException);

            var actualData = (IDictionary)actualInvalidException.Data;

            actualData.Keys.Cast<string>().Should().Contain(nameof(SaleOffer.OfferPrice));
            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleOfferValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSaleOfferAsync(It.IsAny<SaleOffer>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfSaleOfferIsNullAndLogItAsync()
        {
            // given
            SaleOffer nullSaleOffer = null;
            var nullSaleOfferException = new NullSaleOfferException();

            var expectedSaleOfferValidationException =
                new SaleOfferValidationException(nullSaleOfferException);

            // when
            ValueTask<SaleOffer> modifySaleOfferTask =
                this.saleOfferService.ModifySaleOfferAsync(nullSaleOffer);

            SaleOfferValidationException actualSaleOfferValidationException =
                await Assert.ThrowsAsync<SaleOfferValidationException>(
                    modifySaleOfferTask.AsTask);

            // then
            actualSaleOfferValidationException.Should()
                .BeEquivalentTo(expectedSaleOfferValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleOfferValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateSaleOfferAsync(It.IsAny<SaleOffer>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidSaleOfferId = Guid.Empty;
            var invalidSaleOfferException = new InvalidSaleOfferException();

            invalidSaleOfferException.UpsertDataList(
                key: nameof(SaleOffer.Id),
                value: "Id is required");

            var expectedSaleOfferValidationException =
                new SaleOfferValidationException(invalidSaleOfferException);

            // when
            ValueTask<SaleOffer> retrieveSaleOfferTask =
                this.saleOfferService.RetrieveSaleOfferByIdAsync(invalidSaleOfferId);

            SaleOfferValidationException actualSaleOfferValidationException =
                await Assert.ThrowsAsync<SaleOfferValidationException>(
                    retrieveSaleOfferTask.AsTask);

            // then
            actualSaleOfferValidationException.Should()
                .BeEquivalentTo(expectedSaleOfferValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleOfferValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSaleOfferByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfSaleOfferNotFoundAndLogItAsync()
        {
            // given
            Guid someSaleOfferId = Guid.NewGuid();
            SaleOffer nullSaleOffer = null;

            var notFoundSaleOfferException =
                new NotFoundSaleOfferException(someSaleOfferId);

            var expectedSaleOfferValidationException =
                new SaleOfferValidationException(notFoundSaleOfferException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSaleOfferByIdAsync(someSaleOfferId))
                    .ReturnsAsync(nullSaleOffer);

            // when
            ValueTask<SaleOffer> retrieveSaleOfferTask =
                this.saleOfferService.RetrieveSaleOfferByIdAsync(someSaleOfferId);

            SaleOfferValidationException actualSaleOfferValidationException =
                await Assert.ThrowsAsync<SaleOfferValidationException>(
                    retrieveSaleOfferTask.AsTask);

            // then
            actualSaleOfferValidationException.Should()
                .BeEquivalentTo(expectedSaleOfferValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSaleOfferByIdAsync(someSaleOfferId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleOfferValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidSaleOfferId = Guid.Empty;
            var invalidSaleOfferException = new InvalidSaleOfferException();

            invalidSaleOfferException.UpsertDataList(
                key: nameof(SaleOffer.Id),
                value: "Id is required");

            var expectedSaleOfferValidationException =
                new SaleOfferValidationException(invalidSaleOfferException);

            // when
            ValueTask<SaleOffer> removeSaleOfferTask =
                this.saleOfferService.RemoveSaleOfferByIdAsync(invalidSaleOfferId);

            SaleOfferValidationException actualSaleOfferValidationException =
                await Assert.ThrowsAsync<SaleOfferValidationException>(
                    removeSaleOfferTask.AsTask);

            // then
            actualSaleOfferValidationException.Should()
                .BeEquivalentTo(expectedSaleOfferValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleOfferValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSaleOfferByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteSaleOfferAsync(It.IsAny<SaleOffer>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}