//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sheenam.Blazor.Models.Foundations.Homes;
using Sheenam.Blazor.Services.Foundations.Homes;

namespace Sheenam.Blazor.Views.Components
{
    public partial class HomeComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<Home> OnEditSelected { get; set; }

        [Inject]
        public IHomeService HomeService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public IEnumerable<Home> Homes { get; private set; }
        public string ErrorMessage { get; private set; }
        public bool IsLoading { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadHomesAsync();
        }

        public async Task RefreshAsync()
        {
            await LoadHomesAsync();
            this.StateHasChanged();
        }

        private async Task LoadHomesAsync()
        {
            try
            {
                this.IsLoading = true;
                this.ErrorMessage = null;
                this.Homes = await this.HomeService.RetrieveAllHomesAsync();
            }
            catch (Exception ex)
            {
                this.ErrorMessage = "Ma'lumotlarni yuklashda xatolik yuz berdi.";
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        private async Task DeleteHome(Guid homeId)
        {
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Ushbu uyni o'chirmoqchimisiz?");

            if (confirmed)
            {
                try
                {
                    await this.HomeService.RemoveHomeByIdAsync(homeId);
                    this.Homes = this.Homes.Where(h => h.Id != homeId).ToList();
                    this.StateHasChanged();
                }
                catch (Exception ex)
                {
                    this.ErrorMessage = "O'chirishda xatolik yuz berdi.";
                }
            }
        }
        private string GetHouseTypeDisplay(HouseType type) => type switch
        {
            HouseType.Flat => "Kvartira",
            HouseType.Bungalow => "Bir qavatli hovli",
            HouseType.Duplex => "Dupleks (2 qavatli)",
            HouseType.Villa => "Villa/Dacha",
            HouseType.Townhouse => "Taunxaus",
            HouseType.Studio => "Studiya",
            HouseType.Penthouse => "Pentxaus",
            HouseType.Cottage => "Kottej",
            _ => "Boshqa"
        };
    }
}