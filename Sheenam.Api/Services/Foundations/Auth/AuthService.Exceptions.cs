//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Auth.Exceptions;
using Sheenam.Api.Models.Foundations.Users;
using Sheenam.Api.Models.Foundations.Users.Exceptions;
using System;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.Auth
{
    public partial class AuthService
    {
        private delegate ValueTask<User> ReturningUserFunction();
        private delegate ValueTask<string> ReturningTokenFunction();

        private async ValueTask<User> TryCatch(ReturningUserFunction returningUserFunction)
        {
            try
            {
                return await returningUserFunction();
            }
            catch (NullAuthInputException nullAuthInputException)
            {
                throw CreateAndLogValidationException(nullAuthInputException);
            }
            catch (InvalidAuthInputException invalidAuthInputException)
            {
                throw CreateAndLogValidationException(invalidAuthInputException);
            }
            catch (UserValidationException userValidationException)
            {
                throw CreateAndLogDependencyValidationException(userValidationException);
            }
            catch (Exception exception)
            {
                var failedAuthServiceException = new FailedAuthServiceException(exception);

                throw CreateAndLogServiceException(failedAuthServiceException);
            }
        }

        private async ValueTask<string> TryCatch(ReturningTokenFunction returningTokenFunction)
        {
            try
            {
                return await returningTokenFunction();
            }
            catch (NullAuthInputException nullAuthInputException)
            {
                throw CreateAndLogValidationException(nullAuthInputException);
            }
            catch (InvalidAuthInputException invalidAuthInputException)
            {
                throw CreateAndLogValidationException(invalidAuthInputException);
            }
            catch (InvalidCredentialsException invalidCredentialsException)
            {
                throw CreateAndLogValidationException(invalidCredentialsException);
            }
            catch (NotFoundUserException notFoundUserException)
            {
                throw CreateAndLogValidationException(notFoundUserException);
            }
            catch (UserValidationException userValidationException)
            {
                throw CreateAndLogDependencyValidationException(userValidationException);
            }
            catch (Exception exception)
            {
                var failedAuthServiceException = new FailedAuthServiceException(exception);

                throw CreateAndLogServiceException(failedAuthServiceException);
            }
        }

        private AuthValidationException CreateAndLogValidationException(Xeption exception)
        {
            var authValidationException = new AuthValidationException(exception);

            return authValidationException;
        }

        private AuthDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var authDependencyValidationException = new AuthDependencyValidationException(exception);

            return authDependencyValidationException;
        }

        private AuthServiceException CreateAndLogServiceException(Xeption exception)
        {
            var authServiceException = new AuthServiceException(exception);

            return authServiceException;
        }
    }
}