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
            Console.WriteLine("🌐 [AuthService] Sending login request...");
            Console.WriteLine($"🌐 [AuthService] Base URL: {httpClient.BaseAddress}");
            Console.WriteLine($"🌐 [AuthService] Email: {loginRequest.Email}");

            var response = await httpClient.PostAsJsonAsync("api/users/login", loginRequest);

            Console.WriteLine($"🌐 [AuthService] Response status: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                {
                    Console.WriteLine($"✅ [AuthService] Token received (length: {loginResponse.Token.Length})");
                    Console.WriteLine($"✅ [AuthService] Message: {loginResponse.Message}");

                    // Token'ni saqlash
                    await tokenStorage.SetTokenAsync(loginResponse.Token);

                    Console.WriteLine("✅ [AuthService] Token saved to storage");

                    return loginResponse;
                }
                else
                {
                    Console.WriteLine("❌ [AuthService] LoginResponse is null or token is empty");
                }
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ [AuthService] Login failed: {response.StatusCode}");
                Console.WriteLine($"❌ [AuthService] Error: {errorContent}");
            }

            return null;
        }

        public async Task<User> RegisterAsync(RegisterRequest registerRequest)
        {
            Console.WriteLine("🌐 [AuthService] Sending register request...");
            Console.WriteLine($"🌐 [AuthService] Email: {registerRequest.Email}");

            var response = await httpClient.PostAsJsonAsync("api/users/register", registerRequest);

            Console.WriteLine($"🌐 [AuthService] Response status: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<User>();
                Console.WriteLine($"✅ [AuthService] User registered:  {user?.Email}");
                return user;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ [AuthService] Register failed: {response.StatusCode}");
                Console.WriteLine($"❌ [AuthService] Error: {errorContent}");
            }

            return null;
        }

        public async Task LogoutAsync()
        {
            Console.WriteLine("🚪 [AuthService] Logging out...");
            await tokenStorage.RemoveTokenAsync();
            Console.WriteLine("✅ [AuthService] Token removed from storage");
        }

        public async Task<string> GetTokenAsync()
        {
            var token = await tokenStorage.GetTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                Console.WriteLine($"🔑 [AuthService] Token retrieved (length: {token.Length})");
            }
            else
            {
                Console.WriteLine("⚠️ [AuthService] No token found in storage");
            }

            return token;
        }
    }
}