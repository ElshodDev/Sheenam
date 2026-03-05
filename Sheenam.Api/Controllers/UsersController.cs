//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations.Auth.Exceptions;
using Sheenam.Api.Models.Foundations.Users;
using Sheenam.Api.Models.Foundations.Users.Exceptions;
using Sheenam.Api.Services.Foundations.Auth;
using Sheenam.Api.Services.Foundations.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : RESTFulController
    {
        private readonly IUserService userService;
        private readonly IAuthService authService;

        public UsersController(
            IUserService userService,
            IAuthService authService)
        {
            this.userService = userService;
            this.authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async ValueTask<ActionResult<User>> PostUserAsync(RegisterRequest request)
        {
            try
            {
                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Role = UserRole.Guest
                };

                User registeredUser = await this.authService.RegisterAsync(user, request.Password);
                return Created(registeredUser);
            }
            catch (UserValidationException userValidationException)
            {
                return BadRequest(userValidationException.InnerException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
                when (userDependencyValidationException.InnerException is AlreadyExistsUserException)
            {
                return Conflict(userDependencyValidationException.InnerException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
            {
                return BadRequest(userDependencyValidationException.InnerException);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async ValueTask<ActionResult<LoginResponse>> PostLoginAsync(LoginRequest request)
        {
            try
            {
                string token = await this.authService.LoginAsync(request.Email, request.Password);

                var response = new LoginResponse
                {
                    Token = token,
                    Message = "Login successful"
                };

                return Ok(response);
            }
            catch (AuthValidationException authValidationException)
            {
                return BadRequest(authValidationException.InnerException);
            }
            catch (AuthDependencyValidationException authDependencyValidationException)
            {
                return BadRequest(authDependencyValidationException.InnerException);
            }
            catch (AuthServiceException authServiceException)
            {
                return InternalServerError(authServiceException);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IQueryable<User>> GetAllUsers()
        {
            try
            {
                IQueryable<User> allUsers = this.userService.RetrieveAllUsers();
                return Ok(allUsers);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException);
            }
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async ValueTask<ActionResult<User>> GetUserByIdAsync(Guid userId)
        {
            try
            {
                User user = await this.userService.RetrieveUserByIdAsync(userId);
                return Ok(user);
            }
            catch (UserValidationException userValidationException)
                when (userValidationException.InnerException is NotFoundUserException)
            {
                return NotFound(userValidationException.InnerException);
            }
            catch (UserValidationException userValidationException)
            {
                return BadRequest(userValidationException.InnerException);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException);
            }
        }
    }
}