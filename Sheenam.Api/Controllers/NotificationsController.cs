//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Notifications;
using Sheenam.Api.Models.Foundations.Notifications.Exceptions;
using Sheenam.Api.Services.Foundations.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : RESTFulController
    {
        private readonly INotificationService notificationService;

        public NotificationsController(INotificationService notificationService) =>
            this.notificationService = notificationService;

        [HttpPost]
        public async ValueTask<ActionResult<Notification>> PostNotificationAsync(Notification notification)
        {
            try
            {
                Notification addedNotification =
                    await this.notificationService.AddNotificationAsync(notification);

                return Created(addedNotification);
            }
            catch (NotificationValidationException notificationValidationException)
            {
                return BadRequest(notificationValidationException.InnerException);
            }
            catch (NotificationDependencyValidationException notificationDependencyValidationException)
                when (notificationDependencyValidationException.InnerException is AlreadyExistsNotificationException)
            {
                return Conflict(notificationDependencyValidationException.InnerException);
            }
            catch (NotificationDependencyValidationException notificationDependencyValidationException)
            {
                return BadRequest(notificationDependencyValidationException.InnerException);
            }
            catch (NotificationDependencyException notificationDependencyException)
            {
                return InternalServerError(notificationDependencyException);
            }
            catch (NotificationServiceException notificationServiceException)
            {
                return InternalServerError(notificationServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Notification>> GetAllNotifications()
        {
            try
            {
                IQueryable<Notification> allNotifications =
                    this.notificationService.RetrieveAllNotifications();

                return Ok(allNotifications);
            }
            catch (NotificationDependencyException notificationDependencyException)
            {
                return InternalServerError(notificationDependencyException);
            }
            catch (NotificationServiceException notificationServiceException)
            {
                return InternalServerError(notificationServiceException);
            }
        }

        [HttpGet("{notificationId}")]
        public async ValueTask<ActionResult<Notification>> GetNotificationByIdAsync(Guid notificationId)
        {
            try
            {
                Notification notification =
                    await this.notificationService.RetrieveNotificationByIdAsync(notificationId);

                return Ok(notification);
            }
            catch (NotificationValidationException notificationValidationException)
                when (notificationValidationException.InnerException is NotFoundNotificationException)
            {
                return NotFound(notificationValidationException.InnerException);
            }
            catch (NotificationValidationException notificationValidationException)
            {
                return BadRequest(notificationValidationException.InnerException);
            }
            catch (NotificationDependencyException notificationDependencyException)
            {
                return InternalServerError(notificationDependencyException.InnerException);
            }
            catch (NotificationServiceException notificationServiceException)
            {
                return InternalServerError(notificationServiceException.InnerException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Notification>> PutNotificationAsync(Notification notification)
        {
            try
            {
                Notification modifiedNotification =
                    await this.notificationService.ModifyNotificationAsync(notification);

                return Ok(modifiedNotification);
            }
            catch (NotificationValidationException notificationValidationException)
                when (notificationValidationException.InnerException is NotFoundNotificationException)
            {
                return NotFound(notificationValidationException.InnerException);
            }
            catch (NotificationValidationException notificationValidationException)
            {
                return BadRequest(notificationValidationException.InnerException);
            }
            catch (NotificationDependencyValidationException notificationDependencyValidationException)
            {
                return BadRequest(notificationDependencyValidationException.InnerException);
            }
            catch (NotificationDependencyException notificationDependencyException)
            {
                return InternalServerError(notificationDependencyException.InnerException);
            }
            catch (NotificationServiceException notificationServiceException)
            {
                return InternalServerError(notificationServiceException.InnerException);
            }
        }

        [HttpDelete("{notificationId}")]
        public async ValueTask<ActionResult<Notification>> DeleteNotificationByIdAsync(Guid notificationId)
        {
            try
            {
                Notification deletedNotification =
                    await this.notificationService.RemoveNotificationByIdAsync(notificationId);

                return Ok(deletedNotification);
            }
            catch (NotificationValidationException notificationValidationException)
                when (notificationValidationException.InnerException is NotFoundNotificationException)
            {
                return NotFound(notificationValidationException.InnerException);
            }
            catch (NotificationValidationException notificationValidationException)
            {
                return BadRequest(notificationValidationException.InnerException);
            }
            catch (NotificationDependencyValidationException notificationDependencyValidationException)
            {
                return BadRequest(notificationDependencyValidationException.InnerException);
            }
            catch (NotificationDependencyException notificationDependencyException)
            {
                return InternalServerError(notificationDependencyException.InnerException);
            }
            catch (NotificationServiceException notificationServiceException)
            {
                return InternalServerError(notificationServiceException.InnerException);
            }
        }
    }
}