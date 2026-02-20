//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Services.Foundations.Hosts;
using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Views.Components
{
    public partial class HostComponent : ComponentBase
    {
        [Parameter]
        public EventCallback<HostModel> OnEditSelected { get; set; }

        [Inject]
        public IHostService HostService { get; set; }

        public IEnumerable<HostModel> Hosts { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadHostsAsync();
        }

        public async Task RefreshAsync()
        {
            await LoadHostsAsync();
            this.StateHasChanged();
        }

        private async Task LoadHostsAsync()
        {
            try
            {
                this.Hosts = await this.HostService.RetrieveAllHostsAsync();
            }
            catch (Exception exception)
            {
                this.ErrorMessage = "Ma'lumotlarni yuklashda xatolik yuz berdi.";
            }
        }

        private async Task DeleteHost(Guid hostId)
        {
            try
            {
                await this.HostService.RemoveHostByIdAsync(hostId);
                await RefreshAsync();
            }
            catch (Exception exception)
            {
                this.ErrorMessage = "O'chirishda xatolik yuz berdi.";
            }
        }
    }
}