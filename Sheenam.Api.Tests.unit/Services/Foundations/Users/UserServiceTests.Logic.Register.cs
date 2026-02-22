//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Users;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public async Task ShouldRegisterUserAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTimeOffset();
            string inputPassword = GetRandomPassword();

            User randomUser = CreateRandomUser();
            User inputUser = randomUser;

            inputUser.Id = Guid.Empty;
            inputUser.PasswordHash = null;
            inputUser.CreatedDate = default;
            inputUser.UpdatedDate = default;

            User storageUser = inputUser.DeepClone();
            storageUser.Id = Guid.NewGuid();
            storageUser.CreatedDate = randomDateTime;
            storageUser.UpdatedDate = randomDateTime;
            storageUser.PasswordHash = GetRandomString();

            User expectedUser = storageUser.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertUserAsync(It.Is<User>(u =>
                    u.Id != Guid.Empty &&
                    u.PasswordHash != null &&
                    u.CreatedDate != default &&
                    u.UpdatedDate != default)))
                .ReturnsAsync(storageUser);

            // when
            User actualUser = await this.userService
                .RegisterUserAsync(inputUser, inputPassword);

            // then
            actualUser.Should().BeEquivalentTo(expectedUser);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertUserAsync(It.IsAny<User>()),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        private static User CreateRandomUser()
        {
            return new User
            {
                FirstName = GetRandomString(),
                LastName = GetRandomString(),
                Email = $"{GetRandomString()}@test.com",
                PhoneNumber = "+998901234567",
                Role = UserRole.Guest
            };
        }

    }
}