//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Users;
using Sheenam.Api.Models.Foundations.Users.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.Users
{
    public partial class UserService
    {
        private void ValidateUserOnRegister(User user, string password)
        {
            ValidateUserIsNotNull(user);

            Validate(
                (Rule: IsInvalid(user.FirstName), Parameter: nameof(User.FirstName)),
                (Rule: IsInvalid(user.LastName), Parameter: nameof(User.LastName)),
                (Rule: IsInvalid(user.Email), Parameter: nameof(User.Email)),
                (Rule: IsInvalid(password), Parameter: "Password"),
                (Rule: IsInvalidEmail(user.Email), Parameter: nameof(User.Email)),
                (Rule: IsInvalidLength(password, 6), Parameter: "Password")
            );
        }

        private void ValidateUserOnModify(User user)
        {
            ValidateUserIsNotNull(user);

            Validate(
                (Rule: IsInvalid(user.Id), Parameter: nameof(User.Id)),
                (Rule: IsInvalid(user.FirstName), Parameter: nameof(User.FirstName)),
                (Rule: IsInvalid(user.LastName), Parameter: nameof(User.LastName)),
                (Rule: IsInvalid(user.Email), Parameter: nameof(User.Email)),
                (Rule: IsInvalid(user.PasswordHash), Parameter: nameof(User.PasswordHash)),
                (Rule: IsInvalidEmail(user.Email), Parameter: nameof(User.Email))
            );
        }

        private void ValidateUserId(Guid userId)
        {
            Validate((Rule: IsInvalid(userId), Parameter: nameof(User.Id)));
        }

        private void ValidateEmail(string email)
        {
            Validate(
                (Rule: IsInvalid(email), Parameter: nameof(User.Email)),
                (Rule: IsInvalidEmail(email), Parameter: nameof(User.Email))
            );
        }

        private void ValidatePasswordOnVerify(string password, string passwordHash)
        {
            Validate(
                (Rule: IsInvalid(password), Parameter: "Password"),
                (Rule: IsInvalid(passwordHash), Parameter: "PasswordHash")
            );
        }

        private void ValidateStorageUser(User maybeUser, Guid userId)
        {
            if (maybeUser is null)
            {
                throw new NotFoundUserException(userId);
            }
        }

        private void ValidateStorageUserByEmail(User maybeUser, string email)
        {
            if (maybeUser is null)
            {
                throw new NotFoundUserException(email);
            }
        }

        private void ValidateUserIsNotNull(User user)
        {
            if (user is null)
            {
                throw new NullUserException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required."
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required."
        };

        private static dynamic IsInvalidEmail(string email) => new
        {
            Condition = !IsValidEmail(email),
            Message = "Email is invalid."
        };

        private static dynamic IsInvalidLength(string text, int minLength) => new
        {
            Condition = text?.Length < minLength,
            Message = $"Text must be at least {minLength} characters."
        };

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidUserException = new InvalidUserException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidUserException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidUserException.ThrowIfContainsErrors();
        }
    }
}