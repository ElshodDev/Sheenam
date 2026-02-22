//===================================================
//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Services.Foundations.Guests;

namespace Sheenam.Blazor.Views.Components
{
    public partial class GuestComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<Guest> OnEditSelected { get; set; }

        [Inject]
        public IGuestService GuestService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public IEnumerable<Guest> Guests { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadGuestsAsync();
        }

        public async Task RefreshAsync()
        {
            await LoadGuestsAsync();
            this.StateHasChanged();
        }

        private async Task LoadGuestsAsync()
        {
            try
            {
                this.Guests = await this.GuestService.RetrieveAllGuestsAsync();
            }
            catch (Exception)
            {
                this.ErrorMessage = "Ma'lumotlarni yuklashda xatolik yuz berdi.";
            }
        }

        private async Task DeleteGuest(Guid guestId)
        {
            bool confirmed = false;

            try
            {
                confirmed = await this.JSRuntime.InvokeAsync<bool>(
                    "confirm",
                    "Ushbu mehmonni o'chirmoqchimisiz?");
            }
            catch (Exception)
            {
                this.ErrorMessage = "Tasdiqlash oynasini ko'rsatishda xatolik yuz berdi.";
                return;
            }
            if (confirmed)
            {
                try
                {
                    await this.GuestService.RemoveGuestByIdAsync(guestId);
                    await RefreshAsync();
                }
                catch (Exception)
                {
                    this.ErrorMessage = "O'chirishda xatolik yuz berdi.";
                }
            }
        }
    }
}