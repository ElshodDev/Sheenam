//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Favorites;
using Sheenam.Api.Models.Foundations.Favorites.Exceptions;
using Sheenam.Api.Services.Foundations.Favorites;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : RESTFulController
    {
        private readonly IFavoriteService favoriteService;

        public FavoritesController(IFavoriteService favoriteService) =>
            this.favoriteService = favoriteService;

        [HttpPost]
        public async ValueTask<ActionResult<Favorite>> PostFavoriteAsync(Favorite favorite)
        {
            try
            {
                Favorite addedFavorite =
                    await this.favoriteService.AddFavoriteAsync(favorite);

                return Created(addedFavorite);
            }
            catch (FavoriteValidationException favoriteValidationException)
            {
                return BadRequest(favoriteValidationException.InnerException);
            }
            catch (FavoriteDependencyValidationException favoriteDependencyValidationException)
                when (favoriteDependencyValidationException.InnerException
                    is AlreadyExistFavoriteException)
            {
                return Conflict(favoriteDependencyValidationException.InnerException);
            }
            catch (FavoriteDependencyValidationException favoriteDependencyValidationException)
            {
                return BadRequest(favoriteDependencyValidationException.InnerException);
            }
            catch (FavoriteDependencyException favoriteDependencyException)
            {
                return InternalServerError(favoriteDependencyException);
            }
            catch (FavoriteServiceException favoriteServiceException)
            {
                return InternalServerError(favoriteServiceException);
            }
        }

        [HttpGet("user/{userId}")]
        public ActionResult<IQueryable<Favorite>> GetFavoritesByUserId(Guid userId)
        {
            try
            {
                IQueryable<Favorite> favorites =
                    this.favoriteService.RetrieveFavoritesByUserId(userId);

                return Ok(favorites);
            }
            catch (FavoriteDependencyException favoriteDependencyException)
            {
                return InternalServerError(favoriteDependencyException);
            }
            catch (FavoriteServiceException favoriteServiceException)
            {
                return InternalServerError(favoriteServiceException);
            }
        }

        [HttpGet("{favoriteId}")]
        public async ValueTask<ActionResult<Favorite>> GetFavoriteByIdAsync(Guid favoriteId)
        {
            try
            {
                Favorite favorite =
                    await this.favoriteService.RetrieveFavoriteByIdAsync(favoriteId);

                return Ok(favorite);
            }
            catch (FavoriteValidationException favoriteValidationException)
                when (favoriteValidationException.InnerException is NotFoundFavoriteException)
            {
                return NotFound(favoriteValidationException.InnerException);
            }
            catch (FavoriteValidationException favoriteValidationException)
            {
                return BadRequest(favoriteValidationException.InnerException);
            }
            catch (FavoriteDependencyException favoriteDependencyException)
            {
                return InternalServerError(favoriteDependencyException);
            }
            catch (FavoriteServiceException favoriteServiceException)
            {
                return InternalServerError(favoriteServiceException);
            }
        }

        [HttpDelete("{favoriteId}")]
        public async ValueTask<ActionResult<Favorite>> DeleteFavoriteByIdAsync(Guid favoriteId)
        {
            try
            {
                Favorite deletedFavorite =
                    await this.favoriteService.RemoveFavoriteByIdAsync(favoriteId);

                return Ok(deletedFavorite);
            }
            catch (FavoriteValidationException favoriteValidationException)
                when (favoriteValidationException.InnerException is NotFoundFavoriteException)
            {
                return NotFound(favoriteValidationException.InnerException);
            }
            catch (FavoriteValidationException favoriteValidationException)
            {
                return BadRequest(favoriteValidationException.InnerException);
            }
            catch (FavoriteDependencyException favoriteDependencyException)
            {
                return InternalServerError(favoriteDependencyException);
            }
            catch (FavoriteServiceException favoriteServiceException)
            {
                return InternalServerError(favoriteServiceException);
            }
        }
    }
}