//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sheenam.Api.Models.Foundations.Auth.Exceptions;
using Sheenam.Api.Models.Foundations.Users;
using Sheenam.Api.Services.Foundations.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Auth
{
    public partial class AuthService : IAuthService
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;

        public AuthService(
            IUserService userService,
            IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        public ValueTask<User> RegisterAsync(User user, string password) =>
        TryCatch(async () =>
        {
            ValidateRegisterInput(user, password);

            return await this.userService.RegisterUserAsync(user, password);
        });

        public ValueTask<string> LoginAsync(string email, string password) =>
        TryCatch(async () =>
        {
            ValidateLoginInput(email, password);

            User user = await this.userService.RetrieveUserByEmailAsync(email);

            bool isPasswordValid = this.userService.VerifyPassword(password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new InvalidCredentialsException();
            }

            return GenerateJwtToken(user);
        });

        public string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(this.configuration["JwtSettings:SecretKey"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user. LastName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: this.configuration["JwtSettings:Issuer"],
                audience: this.configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(this.configuration["JwtSettings:ExpirationMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}