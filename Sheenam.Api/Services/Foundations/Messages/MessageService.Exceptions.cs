//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations.Messages;
using Sheenam.Api.Models.Foundations.Messages.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.Messages
{
    public partial class MessageService
    {
        private delegate IQueryable<Message> ReturningMessagesFunction();
        private delegate ValueTask<Message> ReturningMessageFunction();

        private IQueryable<Message> TryCatch(ReturningMessagesFunction returningMessagesFunction)
        {
            try
            {
                return returningMessagesFunction();
            }
            catch (SqlException sqlException)
            {
                var failedStorageException = new FailedMessageStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (Exception exception)
            {
                var failedServiceException = new FailedMessageServiceException(exception);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private async ValueTask<Message> TryCatch(ReturningMessageFunction returningMessageFunction)
        {
            try
            {
                return await returningMessageFunction();
            }
            catch (NullMessageException nullMessageException)
            {
                throw CreateAndLogValidationException(nullMessageException);
            }
            catch (InvalidMessageException invalidMessageException)
            {
                throw CreateAndLogValidationException(invalidMessageException);
            }
            catch (NotFoundMessageException notFoundMessageException)
            {
                throw CreateAndLogValidationException(notFoundMessageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsException =
                    new AlreadyExistsMessageException(duplicateKeyException);
                throw CreateAndLogDependencyValidationException(alreadyExistsException);
            }
            catch (SqlException sqlException)
            {
                var failedStorageException = new FailedMessageStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedStorageException);
            }
            catch (Exception exception)
            {
                var failedServiceException = new FailedMessageServiceException(exception);
                throw CreateAndLogServiceException(failedServiceException);
            }
        }

        private MessageValidationException CreateAndLogValidationException(Xeption exception)
        {
            var messageValidationException = new MessageValidationException(exception);
            this.loggingBroker.LogError(messageValidationException);
            return messageValidationException;
        }

        private MessageDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var messageDependencyException = new MessageDependencyException(exception);
            this.loggingBroker.LogCritical(messageDependencyException);
            return messageDependencyException;
        }

        private MessageDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var messageDependencyValidationException =
                new MessageDependencyValidationException(exception);
            this.loggingBroker.LogError(messageDependencyValidationException);
            return messageDependencyValidationException;
        }

        private MessageServiceException CreateAndLogServiceException(Xeption exception)
        {
            var messageServiceException = new MessageServiceException(exception);
            this.loggingBroker.LogError(messageServiceException);
            return messageServiceException;
        }
    }
}
