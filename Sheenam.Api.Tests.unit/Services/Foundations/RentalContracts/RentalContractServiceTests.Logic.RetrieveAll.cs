//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.RentalContracts;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.RentalContracts
{
    public partial class RentalContractServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllRentalContracts()
        {
            // given
            IQueryable<RentalContract> randomRentalContracts = CreateRandomRentalContracts();
            IQueryable<RentalContract> storageRentalContracts = randomRentalContracts;
            IQueryable<RentalContract> expectedRentalContracts = storageRentalContracts.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllRentalContracts())
                    .Returns(storageRentalContracts);

            // when
            IQueryable<RentalContract> actualRentalContracts =
                this.rentalContractService.RetrieveAllRentalContracts();

            // then
            actualRentalContracts.Should().BeEquivalentTo(expectedRentalContracts);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllRentalContracts(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}