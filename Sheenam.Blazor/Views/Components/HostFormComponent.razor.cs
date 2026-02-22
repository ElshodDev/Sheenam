//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Services.Foundations.Hosts;
using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Views.Components
{
    public partial class HostFormComponent : ComponentBase
    {
        [Parameter]
        public EventCallback OnHostAdded { get; set; }

        [Inject]
        public IHostService HostService { get; set; }

        public HostModel host = new HostModel();

        protected override void OnInitialized()
        {
            ResetForm();
        }

        public void EditHost(HostModel hostToEdit)
        {
            this.host = new HostModel
            {
                Id = hostToEdit.Id,
                FirstName = hostToEdit.FirstName,
                LastName = hostToEdit.LastName,
                Email = hostToEdit.Email,
                PhoneNumber = hostToEdit.PhoneNumber,
                DateOfBirth = hostToEdit.DateOfBirth,
                Gender = hostToEdit.Gender
            };
            StateHasChanged();
        }

        private void ResetForm()
        {
            this.host = new HostModel
            {
                Id = Guid.Empty,
                DateOfBirth = DateTimeOffset.UtcNow.AddYears(-20)
            };
        }

        private async Task HandleSubmit()
        {
            try
            {
                if (this.host.Id == Guid.Empty)
                {
                    this.host.Id = Guid.NewGuid();
                    await this.HostService.AddHostAsync(this.host);
                }
                else
                {
                    await this.HostService.ModifyHostAsync(this.host);
                }

                await OnHostAdded.InvokeAsync();
                ResetForm();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}