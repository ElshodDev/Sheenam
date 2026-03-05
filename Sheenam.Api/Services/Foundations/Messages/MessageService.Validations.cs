//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Messages;
using Sheenam.Api.Models.Foundations.Messages.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.Messages
{
    public partial class MessageService
    {
        private void ValidateMessageOnAdd(Message message)
        {
            ValidateMessageNotNull(message);

            Validate(
                (Rule: IsInvalid(message.Id), Parameter: nameof(Message.Id)),
                (Rule: IsInvalid(message.SenderId), Parameter: nameof(Message.SenderId)),
                (Rule: IsInvalid(message.ReceiverId), Parameter: nameof(Message.ReceiverId)),
                (Rule: IsInvalid(message.Content), Parameter: nameof(Message.Content)),
                (Rule: IsInvalid(message.SentDate), Parameter: nameof(Message.SentDate)));
        }

        private void ValidateMessageOnModify(Message message)
        {
            ValidateMessageNotNull(message);

            Validate(
                (Rule: IsInvalid(message.Id), Parameter: nameof(Message.Id)),
                (Rule: IsInvalid(message.SenderId), Parameter: nameof(Message.SenderId)),
                (Rule: IsInvalid(message.ReceiverId), Parameter: nameof(Message.ReceiverId)),
                (Rule: IsInvalid(message.Content), Parameter: nameof(Message.Content)));
        }

        private static void ValidateMessageId(Guid messageId) =>
            Validate((Rule: IsInvalid(messageId), Parameter: nameof(Message.Id)));

        private static void ValidateStorageMessage(Message maybeMessage, Guid messageId)
        {
            if (maybeMessage is null)
                throw new NotFoundMessageException(messageId);
        }

        private static void ValidateMessageNotNull(Message message)
        {
            if (message is null)
                throw new NullMessageException();
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidMessageException = new InvalidMessageException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                    invalidMessageException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
            }

            invalidMessageException.ThrowIfContainsErrors();
        }
    }
}
