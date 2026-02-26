//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;
using Sheenam.Api.Services.Foundations.Guests;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : RESTFulController
    {
        private readonly IGuestService guestService;

        public GuestsController(IGuestService guestService)
        {
            this.guestService = guestService;
        }
        [HttpPost]
        public async ValueTask<IActionResult> PostGuestAsync(Guest guest)
        {
            try
            {
                Guest postedGuest = await this.guestService.AddGuestAsync(guest);

                return Created(postedGuest);
            }
            catch (GuestValidationException guestValidationException)
            {
                return BadRequest(guestValidationException.InnerException);
            }
            catch (GuestDependecyValidationException guestDependecyValidationException)
             when (guestDependecyValidationException.InnerException is AlreadyExistGuestException)
            {
                return Conflict(guestDependecyValidationException.InnerException);
            }
            catch (GuestDependecyValidationException guestDependecyValidationException)
            {
                return BadRequest(guestDependecyValidationException.InnerException);
            }
            catch (GuestDependecyException guestDependecyException)
            {
                return InternalServerError(guestDependecyException.InnerException);
            }
            catch (GuestServiceException guestServiceException)
            {
                return InternalServerError(guestServiceException.InnerException);
            }
        }
        [HttpGet]
        public ActionResult<IQueryable<Guest>> GetAllGuests()
        {
            try
            {
                IQueryable<Guest> allGuests = this.guestService.RetrieveAllGuests();
                return Ok(allGuests);
            }
            catch (GuestDependecyException guestDependecyException)
            {
                return InternalServerError(guestDependecyException.InnerException);
            }
            catch (GuestServiceException guestServiceException)
            {
                return InternalServerError(guestServiceException.InnerException);
            }
        }
        [HttpGet("{guestId}")]
        public async ValueTask<ActionResult<Guest>> GetGuestByIdAsync(System.Guid guestId)
        {
            try
            {
                Guest guest = await this.guestService.RetrieveGuestByIdAsync(guestId);
                return Ok(guest);
            }
            catch (GuestValidationException guestValidationException)
             when (guestValidationException.InnerException is NotFoundGuestException)
            {
                return NotFound(guestValidationException.InnerException);
            }
            catch (GuestValidationException guestValidationException)
            {
                return BadRequest(guestValidationException.InnerException);
            }
            catch (GuestDependecyException guestDependecyException)
            {
                return InternalServerError(guestDependecyException.InnerException);
            }
            catch (GuestServiceException guestServiceException)
            {
                return InternalServerError(guestServiceException.InnerException);
            }
        }

        [HttpPut("{guestId}")]
        public async ValueTask<ActionResult<Guest>> PutGuestAsync(System.Guid guestId, Guest guest)
        {
            try
            {
                if (guestId != guest.Id)
                {
                    return BadRequest("Guest ID mismatch");
                }

                Guest updatedGuest = await this.guestService.ModifyGuestAsync(guest);

                return Ok(updatedGuest);
            }
            catch (GuestValidationException guestValidationException)
             when (guestValidationException.InnerException is NotFoundGuestException)
            {
                return NotFound(guestValidationException.InnerException);
            }
            catch (GuestValidationException guestValidationException)
            {
                return BadRequest(guestValidationException.InnerException);
            }
            catch (GuestDependecyException guestDependecyException)
            {
                return InternalServerError(guestDependecyException.InnerException);
            }
            catch (GuestServiceException guestServiceException)
            {
                return InternalServerError(guestServiceException.InnerException);
            }
        }
        [HttpDelete("{guestId}")]
        public async ValueTask<ActionResult<Guest>> DeleteGuestByIdAsync(System.Guid guestId)
        {
            try
            {
                Guest deletedGuest = await this.guestService.RemoveGuestByIdAsync(guestId);
                return Ok(deletedGuest);
            }
            catch (GuestValidationException guestValidationException)
             when (guestValidationException.InnerException is NotFoundGuestException)
            {
                return NotFound(guestValidationException.InnerException);
            }
            catch (GuestValidationException guestValidationException)
            {
                return BadRequest(guestValidationException.InnerException);
            }
            catch (GuestDependecyException guestDependecyException)
            {
                return InternalServerError(guestDependecyException.InnerException);
            }
            catch (GuestServiceException guestServiceException)
            {
                return InternalServerError(guestServiceException.InnerException);
            }
        }
    }
}
