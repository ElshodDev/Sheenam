//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
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
            catch (Exception exception)
            {
                this.ErrorMessage = "Ma'lumotlarni yuklashda xatolik yuz berdi.";
            }
        }

        private async Task DeleteGuest(Guid guestId)
        {
            try
            {
                await this.GuestService.RemoveGuestByIdAsync(guestId);
                await RefreshAsync();
            }
            catch (Exception exception)
            {
                this.ErrorMessage = "O'chirishda xatolik yuz berdi.";
            }
        }
    }
}