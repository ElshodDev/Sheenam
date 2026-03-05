//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Messages;
using Sheenam.Api.Models.Foundations.Messages.Exceptions;
using Sheenam.Api.Services.Foundations.Messages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MessagesController : RESTFulController
    {
        private readonly IMessageService messageService;

        public MessagesController(IMessageService messageService) =>
            this.messageService = messageService;

        [HttpPost]
        public async ValueTask<ActionResult<Message>> PostMessageAsync(Message message)
        {
            try
            {
                Message addedMessage =
                    await this.messageService.AddMessageAsync(message);

                return Created(addedMessage);
            }
            catch (MessageValidationException messageValidationException)
            {
                return BadRequest(messageValidationException.InnerException);
            }
            catch (MessageDependencyValidationException messageDependencyValidationException)
                when (messageDependencyValidationException.InnerException
                    is AlreadyExistsMessageException)
            {
                return Conflict(messageDependencyValidationException.InnerException);
            }
            catch (MessageDependencyValidationException messageDependencyValidationException)
            {
                return BadRequest(messageDependencyValidationException.InnerException);
            }
            catch (MessageDependencyException messageDependencyException)
            {
                return InternalServerError(messageDependencyException);
            }
            catch (MessageServiceException messageServiceException)
            {
                return InternalServerError(messageServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Message>> GetAllMessages()
        {
            try
            {
                IQueryable<Message> messages =
                    this.messageService.RetrieveAllMessages();

                return Ok(messages);
            }
            catch (MessageDependencyException messageDependencyException)
            {
                return InternalServerError(messageDependencyException);
            }
            catch (MessageServiceException messageServiceException)
            {
                return InternalServerError(messageServiceException);
            }
        }

        [HttpGet("user/{userId}")]
        public ActionResult<IQueryable<Message>> GetMessagesByUserId(Guid userId)
        {
            try
            {
                IQueryable<Message> messages =
                    this.messageService.RetrieveMessagesByUserId(userId);

                return Ok(messages);
            }
            catch (MessageDependencyException messageDependencyException)
            {
                return InternalServerError(messageDependencyException);
            }
            catch (MessageServiceException messageServiceException)
            {
                return InternalServerError(messageServiceException);
            }
        }

        [HttpGet("{messageId}")]
        public async ValueTask<ActionResult<Message>> GetMessageByIdAsync(Guid messageId)
        {
            try
            {
                Message message =
                    await this.messageService.RetrieveMessageByIdAsync(messageId);

                return Ok(message);
            }
            catch (MessageValidationException messageValidationException)
                when (messageValidationException.InnerException is NotFoundMessageException)
            {
                return NotFound(messageValidationException.InnerException);
            }
            catch (MessageValidationException messageValidationException)
            {
                return BadRequest(messageValidationException.InnerException);
            }
            catch (MessageDependencyException messageDependencyException)
            {
                return InternalServerError(messageDependencyException);
            }
            catch (MessageServiceException messageServiceException)
            {
                return InternalServerError(messageServiceException);
            }
        }

        [HttpPut("{messageId}")]
        public async ValueTask<ActionResult<Message>> PutMessageAsync(
            Guid messageId, Message message)
        {
            try
            {
                message.Id = messageId;
                Message modifiedMessage =
                    await this.messageService.ModifyMessageAsync(message);

                return Ok(modifiedMessage);
            }
            catch (MessageValidationException messageValidationException)
                when (messageValidationException.InnerException is NotFoundMessageException)
            {
                return NotFound(messageValidationException.InnerException);
            }
            catch (MessageValidationException messageValidationException)
            {
                return BadRequest(messageValidationException.InnerException);
            }
            catch (MessageDependencyValidationException messageDependencyValidationException)
            {
                return BadRequest(messageDependencyValidationException.InnerException);
            }
            catch (MessageDependencyException messageDependencyException)
            {
                return InternalServerError(messageDependencyException);
            }
            catch (MessageServiceException messageServiceException)
            {
                return InternalServerError(messageServiceException);
            }
        }

        [HttpDelete("{messageId}")]
        public async ValueTask<ActionResult<Message>> DeleteMessageByIdAsync(Guid messageId)
        {
            try
            {
                Message deletedMessage =
                    await this.messageService.RemoveMessageByIdAsync(messageId);

                return Ok(deletedMessage);
            }
            catch (MessageValidationException messageValidationException)
                when (messageValidationException.InnerException is NotFoundMessageException)
            {
                return NotFound(messageValidationException.InnerException);
            }
            catch (MessageValidationException messageValidationException)
            {
                return BadRequest(messageValidationException.InnerException);
            }
            catch (MessageDependencyException messageDependencyException)
            {
                return InternalServerError(messageDependencyException);
            }
            catch (MessageServiceException messageServiceException)
            {
                return InternalServerError(messageServiceException);
            }
        }
    }
}
