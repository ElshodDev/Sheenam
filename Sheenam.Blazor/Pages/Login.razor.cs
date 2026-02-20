//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Models.Foundations.Users;
using Sheenam.Blazor.Services.Foundations.Users;

namespace Sheenam.Blazor.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; } // LocalStorage'ni inject qiling

        private LoginRequest loginRequest = new LoginRequest();
        private bool isProcessing = false;
        private string errorMessage = string.Empty;

        private async Task HandleLoginAsync()
        {
            this.isProcessing = true;
            this.errorMessage = string.Empty;

            try
            {
                LoginResponse response = await this.UserService.LoginAsync(this.loginRequest);

                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    // 1. Tokenni saqlash
                    await this.LocalStorage.SetItemAsync("accessToken", response.Token);

                    // 2. Token ichidan ID ni olish (response.User o'rniga)
                    // Biz tokenni . orqali qismlarga bo'lamiz va o'rta qismini (payload) o'qiymiz
                    var userId = ParseUserIdFromToken(response.Token);

                    if (!string.IsNullOrEmpty(userId))
                    {
                        await this.LocalStorage.SetItemAsync("currentHostId", userId);
                    }

                    this.NavigationManager.NavigateTo("/");
                }
            }
            catch (Exception ex)
            {
                this.errorMessage = "Email yoki parol xato!";
            }
            finally
            {
                this.isProcessing = false;
            }
        }

        // Tokenni parchalab ID ni topuvchi yordamchi metod
        private string ParseUserIdFromToken(string token)
        {
            try
            {
                var payload = token.Split('.')[1];
                var jsonBytes = ParseBase64WithoutPadding(payload);
                var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

                // JWT ichida ID ko'pincha "nameid" yoki "sub" deb saqlanadi
                return keyValuePairs.FirstOrDefault(x => x.Key == "nameid" || x.Key == "sub").Value?.ToString();
            }
            catch
            {
                return null;
            }
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}