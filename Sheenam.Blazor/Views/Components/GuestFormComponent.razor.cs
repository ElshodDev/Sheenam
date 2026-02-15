using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Services.Foundations.Guests;

namespace Sheenam.Blazor.Views.Components
{
    public partial class GuestFormComponent : ComponentBase
    {
        [Parameter]
        public EventCallback OnGuestAdded { get; set; }

        [Inject]
        public IGuestService GuestService { get; set; }

        public Guest guest = new Guest();

        protected override void OnInitialized()
        {
            ResetForm();
        }

        public void EditGuest(Guest guestToEdit)
        {
            this.guest = new Guest
            {
                Id = guestToEdit.Id,
                FirstName = guestToEdit.FirstName,
                LastName = guestToEdit.LastName,
                Email = guestToEdit.Email,
                PhoneNumber = guestToEdit.PhoneNumber,
                Address = guestToEdit.Address,
                DateOfBirth = guestToEdit.DateOfBirth,
                Gender = guestToEdit.Gender
            };
            StateHasChanged();
        }

        private void ResetForm()
        {
            this.guest = new Guest
            {
                Id = Guid.Empty,
                DateOfBirth = DateTimeOffset.Now
            };
        }

        private async Task HandleSubmit()
        {
            try
            {
                if (this.guest.Id == Guid.Empty)
                {
                    this.guest.Id = Guid.NewGuid();
                    await this.GuestService.AddGuestAsync(this.guest);
                }
                else
                {
                    await this.GuestService.ModifyGuestAsync(this.guest);
                }

                await OnGuestAdded.InvokeAsync();
                ResetForm();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}