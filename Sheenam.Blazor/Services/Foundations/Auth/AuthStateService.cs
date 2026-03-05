//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Blazored.LocalStorage;
using System.Text.Json;

namespace Sheenam.Blazor.Services.Foundations.Auth
{
    public class AuthStateService : IAuthStateService
    {
        private readonly ILocalStorageService localStorage;

        public AuthStateService(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task<string> GetCurrentUserIdAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token)) return null;
            return GetClaimFromToken(token, "sub");
        }

        public async Task<string> GetCurrentUserRoleAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token)) return null;
            return GetClaimFromToken(token, "role");
        }

        public async Task<string> GetCurrentUserEmailAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token)) return null;
            return GetClaimFromToken(token, "email");
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }

        public async Task<bool> IsAdminAsync()
        {
            var role = await GetCurrentUserRoleAsync();
            return role == "Admin";
        }

        public async Task<bool> IsHostAsync()
        {
            var role = await GetCurrentUserRoleAsync();
            return role == "Host";
        }

        public async Task<bool> IsGuestAsync()
        {
            var role = await GetCurrentUserRoleAsync();
            return role == "Guest";
        }

        public async Task LogoutAsync()
        {
            await this.localStorage.RemoveItemAsync("accessToken");
        }

        private async Task<string> GetTokenAsync()
        {
            try
            {
                return await this.localStorage.GetItemAsync<string>("accessToken");
            }
            catch
            {
                return null;
            }
        }

        private string GetClaimFromToken(string token, string claimType)
        {
            try
            {
                var payload = token.Split('.')[1];
                switch (payload.Length % 4)
                {
                    case 2: payload += "=="; break;
                    case 3: payload += "="; break;
                }
                var jsonBytes = Convert.FromBase64String(payload);
                var claims = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonBytes);

                if (claims.TryGetValue(claimType, out var value))
                    return value.ToString();

                if (claimType == "role" && claims.TryGetValue(
                    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                    out var roleValue))
                    return roleValue.ToString();

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
