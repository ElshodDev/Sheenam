//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using Sheenam.Blazor.Models.Foundations.RentalContracts;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.RentalContracts
{
    public partial class RentalContractServiceTests
    {
        [Fact]
        public async Task ShouldAddRentalContractAsync()
        {
            // given
            RentalContract randomRentalContract = CreateRandomRentalContract();
            RentalContract inputRentalContract = randomRentalContract;
            RentalContract retrievedRentalContract = inputRentalContract;
            RentalContract expectedRentalContract = retrievedRentalContract;

            this.apiBrokerMock.Setup(broker =>
                broker.PostRentalContractAsync(inputRentalContract))
                    .ReturnsAsync(retrievedRentalContract);

            // when
            RentalContract actualRentalContract =
                await this.rentalContractService.AddRentalContractAsync(inputRentalContract);

            // then
            actualRentalContract.Should().BeEquivalentTo(expectedRentalContract);

            this.apiBrokerMock.Verify(broker =>
                broker.PostRentalContractAsync(inputRentalContract),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}