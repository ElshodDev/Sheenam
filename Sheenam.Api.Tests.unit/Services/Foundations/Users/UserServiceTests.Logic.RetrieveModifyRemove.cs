//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Users;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllUsers()
        {
            // given
            IQueryable<User> randomUsers = CreateRandomUsers();
            IQueryable<User> storageUsers = randomUsers;
            IQueryable<User> expectedUsers = storageUsers.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllUsers())
                    .Returns(storageUsers);

            // when
            IQueryable<User> actualUsers =
                this.userService.RetrieveAllUsers();

            // then
            actualUsers.Should().BeEquivalentTo(expectedUsers);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllUsers(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveUserByIdAsync()
        {
            // given
            Guid randomUserId = Guid.NewGuid();
            User randomUser = CreateRandomStoredUser();
            User storageUser = randomUser;
            User expectedUser = storageUser.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectUserByIdAsync(randomUserId))
                    .ReturnsAsync(storageUser);

            // when
            User actualUser =
                await this.userService.RetrieveUserByIdAsync(randomUserId);

            // then
            actualUser.Should().BeEquivalentTo(expectedUser);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectUserByIdAsync(randomUserId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyUserAsync()
        {
            // given
            User randomUser = CreateRandomStoredUser();
            User inputUser = randomUser;
            User storageUser = inputUser.DeepClone();
            User expectedUser = storageUser.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectUserByIdAsync(inputUser.Id))
                    .ReturnsAsync(storageUser);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateUserAsync(It.IsAny<User>()))
                    .ReturnsAsync(storageUser);

            // when
            User actualUser =
                await this.userService.ModifyUserAsync(inputUser);

            // then
            actualUser.Should().BeEquivalentTo(expectedUser);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectUserByIdAsync(inputUser.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateUserAsync(It.IsAny<User>()),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveUserByIdAsync()
        {
            // given
            Guid randomUserId = Guid.NewGuid();
            User randomUser = CreateRandomStoredUser();
            User storageUser = randomUser;
            User expectedUser = storageUser.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectUserByIdAsync(randomUserId))
                    .ReturnsAsync(storageUser);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteUserAsync(storageUser))
                    .ReturnsAsync(expectedUser);

            // when
            User actualUser =
                await this.userService.RemoveUserByIdAsync(randomUserId);

            // then
            actualUser.Should().BeEquivalentTo(expectedUser);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectUserByIdAsync(randomUserId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteUserAsync(storageUser),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        private static IQueryable<User> CreateRandomUsers()
        {
            int count = GetRandomNumber();
            var users = new List<User>();

            for (int i = 0; i < count; i++)
                users.Add(CreateRandomStoredUser());

            return users.AsQueryable();
        }

        private static User CreateRandomStoredUser()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = GetRandomString(),
                LastName = GetRandomString(),
                Email = $"{GetRandomString()}@test.com",
                PasswordHash = GetRandomString(),
                PhoneNumber = "+998901234567",
                Role = UserRole.Guest,
                CreatedDate = GetRandomDateTimeOffset(),
                UpdatedDate = GetRandomDateTimeOffset()
            };
        }
    }
}
