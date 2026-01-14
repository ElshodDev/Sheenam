//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

namespace Sheenam.Blazor.Services
{
    public class TokenStorageService
    {
        private string? token;

        public Task SetTokenAsync(string value)
        {
            token = value;
            return Task.CompletedTask;
        }

        public Task<string?> GetTokenAsync()
        {
            return Task.FromResult(token);
        }

        public Task RemoveTokenAsync()
        {
            token = null;
            return Task.CompletedTask;
        }
    }
}