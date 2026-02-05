//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using Sheenam.Api.Models.Foundations.Homes;

namespace Sheenam.Api.Tests.unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public void ShouldRetrieveFilteredHomes()
        {
            // given
            IQueryable<Home> randomHomes = CreateRandomHomes();
            IQueryable<Home> storageHomes = randomHomes;

            var filter = new HomeFilter
            {
                MinPrice = 100,
                MaxPrice = 5000,
                NumberOfRooms = 3
            };

            // Mantiqiy kutish (Expected) - MonthlyRent ishlatamiz
            IQueryable<Home> expectedHomes = storageHomes.Where(home =>
                home.MonthlyRent >= filter.MinPrice &&
                home.MonthlyRent <= filter.MaxPrice &&
                home.NumberOfBedrooms == filter.NumberOfRooms);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Returns(storageHomes);

            // when
            // DIQQAT: Bu yerda endi xato bo'lmasligi kerak
            IQueryable<Home> actualHomes =
                this.homeService.RetrieveAllHomes(filter);

            // then
            actualHomes.Should().BeEquivalentTo(expectedHomes);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomes(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}