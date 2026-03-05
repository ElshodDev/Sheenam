//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Models.Foundations.Users;
using Sheenam.Blazor.Services.Foundations.Users;

namespace Sheenam.Blazor.Pages
{
    public partial class Register : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private RegisterRequest request = new RegisterRequest();
        private bool isProcessing = false;
        private bool isSuccess = false;
        private string errorMessage = string.Empty;

        private async Task HandleRegisterAsync()
        {
            this.isProcessing = true;
            this.errorMessage = string.Empty;

            try
            {
                await this.UserService.RegisterAsync(request);
                this.isSuccess = true;
            }
            catch (Exception)
            {
                this.errorMessage = "Ro'yxatdan o'tishda xatolik! Email allaqachon mavjud bo'lishi mumkin.";
            }
            finally
            {
                this.isProcessing = false;
            }
        }
    }
}
