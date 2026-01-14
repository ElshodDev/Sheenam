//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.Users;
using Sheenam.Api.Models.Foundations.Users.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRegisterIfUserIsNullAndLogItAsync()
        {
            // given
            User nullUser = null;
            string password = GetRandomString();
            var nullUserException = new NullUserException();

            var expectedUserValidationException =
                new UserValidationException(nullUserException);

            // when
            ValueTask<User> registerUserTask =
                this.userService.RegisterUserAsync(nullUser, password);

            UserValidationException actualUserValidationException =
                await Assert.ThrowsAsync<UserValidationException>(
                    registerUserTask.AsTask);

            // then
            Assert.Equal(
                expectedUserValidationException.Message,
                actualUserValidationException.Message);

            Assert.Equal(
              expectedUserValidationException.InnerException.Message,
              actualUserValidationException.InnerException.Message);



            this.storageBrokerMock.Verify(broker =>
                broker.InsertUserAsync(It.IsAny<User>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}