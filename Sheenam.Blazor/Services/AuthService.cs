//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Auth;

namespace Sheenam.Blazor.Services
{
    public class AuthService
    {
        private readonly HttpClient httpClient;
        private readonly TokenStorageService tokenStorage;

        public AuthService(
            HttpClient httpClient,
            TokenStorageService tokenStorage)
        {
            this.httpClient = httpClient;
            this.tokenStorage = tokenStorage;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var response = await httpClient.PostAsJsonAsync("api/users/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                // Token'ni saqlash
                await tokenStorage.SetTokenAsync(loginResponse.Token);

                return loginResponse;
            }

            return null;
        }

        public async Task<User> RegisterAsync(RegisterRequest registerRequest)
        {
            var response = await httpClient.PostAsJsonAsync("api/users/register", registerRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }

            return null;
        }

        public async Task LogoutAsync()
        {
            await tokenStorage.RemoveTokenAsync();
        }

        public async Task<string> GetTokenAsync()
        {
            return await tokenStorage.GetTokenAsync();
        }
    }
}