//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
namespace Sheenam.Blazor.Services.Foundations.Auth
{
    public interface IAuthStateService
    {
        Task<string> GetCurrentUserIdAsync();
        Task<string> GetCurrentUserRoleAsync();
        Task<string> GetCurrentUserEmailAsync();
        Task<bool> IsAuthenticatedAsync();
        Task<bool> IsAdminAsync();
        Task<bool> IsHostAsync();
        Task<bool> IsGuestAsync();
        Task LogoutAsync();
    }
}
