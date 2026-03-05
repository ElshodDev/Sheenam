//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
                IQueryable<Notification> retrievedNotifications =
                    this.notificationService.RetrieveAllNotifications();

                return Ok(retrievedNotifications);
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
    }
}