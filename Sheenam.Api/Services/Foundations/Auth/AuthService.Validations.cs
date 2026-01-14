//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Auth.Exceptions;
using Sheenam.Api.Models.Foundations.Users;

namespace Sheenam.Api.Services.Foundations.Auth
{
    public partial class AuthService
    {
        private void ValidateRegisterInput(User user, string password)
        {
            if (user is null)
            {
                throw new NullAuthInputException("User");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new NullAuthInputException("Password");
            }
        }

        private void ValidateLoginInput(string email, string password)
        {
            Validate(
                (Rule: IsInvalid(email), Parameter: "Email"),
                (Rule: IsInvalid(password), Parameter: "Password")
            );
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required."
        };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidAuthInputException = new InvalidAuthInputException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidAuthInputException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidAuthInputException.ThrowIfContainsErrors();
        }
    }
}