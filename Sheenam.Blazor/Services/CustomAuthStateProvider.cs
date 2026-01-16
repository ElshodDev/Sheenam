//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sheenam.Blazor.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly TokenStorageService tokenStorage;
        private ClaimsPrincipal currentUser = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider(TokenStorageService tokenStorage)
        {
            this.tokenStorage = tokenStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                Console.WriteLine("🔍 [AuthStateProvider] Getting authentication state.. .");

                // ✅ LocalStorage'dan token olish
                var token = await tokenStorage.GetTokenAsync();

                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("❌ [AuthStateProvider] No token found - user is anonymous");
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                // ✅ Token'ni parse qilish
                var handler = new JwtSecurityTokenHandler();

                if (!handler.CanReadToken(token))
                {
                    Console.WriteLine("❌ [AuthStateProvider] Invalid token format");
                    await tokenStorage.RemoveTokenAsync();
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                var jwtToken = handler.ReadJwtToken(token);

                // ✅ Token expiration tekshirish
                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    Console.WriteLine("❌ [AuthStateProvider] Token expired");
                    await tokenStorage.RemoveTokenAsync();
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                // ✅ Claims'larni olish
                var claims = jwtToken.Claims.ToList();

                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                currentUser = user;

                var userName = user.Identity?.Name ??  user.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown";
                Console.WriteLine($"✅ [AuthStateProvider] User authenticated: {userName}");

                return new AuthenticationState(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [AuthStateProvider] Error:  {ex.Message}");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public void NotifyUserAuthentication(string token)
        {
            try
            {
                Console.WriteLine("🔔 [AuthStateProvider] Notifying user authentication...");

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var claims = jwtToken.Claims.ToList();

                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                currentUser = user;

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

                Console.WriteLine("✅ [AuthStateProvider] Authentication state updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [AuthStateProvider] Error notifying authentication: {ex.Message}");
            }
        }

        public void NotifyUserLogout()
        {
            try
            {
                Console.WriteLine("🔔 [AuthStateProvider] Notifying user logout...");

                var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
                currentUser = anonymousUser;

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));

                Console.WriteLine("✅ [AuthStateProvider] Logout notification sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [AuthStateProvider] Error notifying logout: {ex.Message}");
            }
        }
    }
}