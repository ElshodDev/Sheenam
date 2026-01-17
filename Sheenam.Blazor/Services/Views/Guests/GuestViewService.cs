//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Models.ViewModels;
using Sheenam.Blazor.Services.Foundations.Guests;

namespace Sheenam.Blazor.Services.Views.Guests
{
    public class GuestViewService : IGuestViewService
    {
        private readonly IGuestService guestService;

        public GuestViewService(IGuestService guestService)
        {
            this.guestService = guestService;
        }

        public async ValueTask<GuestViewModel> AddGuestViewAsync(Guest guest)
        {
            var guestViewModel = new GuestViewModel { IsSubmitting = true };

            try
            {
                Guest addedGuest = await this.guestService.AddGuestAsync(guest);
                guestViewModel.Guest = addedGuest;
            }
            catch (Exception ex)
            {
                guestViewModel.ErrorMessage = ex.Message;
            }
            finally
            {
                guestViewModel.IsSubmitting = false;
            }

            return guestViewModel;
        }

        public async ValueTask<List<GuestViewModel>> RetrieveAllGuestViewsAsync()
        {
            var guestViewModels = new List<GuestViewModel>();

            try
            {
                List<Guest> guests = await this.guestService.RetrieveAllGuestsAsync();

                guestViewModels = guests.Select(guest => new GuestViewModel
                {
                    Guest = guest,
                    IsLoading = false
                }).ToList();
            }
            catch (Exception ex)
            {
                guestViewModels.Add(new GuestViewModel
                {
                    ErrorMessage = ex.Message,
                    IsLoading = false
                });
            }

            return guestViewModels;
        }

        public async ValueTask<GuestViewModel> RetrieveGuestViewByIdAsync(Guid guestId)
        {
            var guestViewModel = new GuestViewModel { IsLoading = true };

            try
            {
                Guest guest = await this.guestService.RetrieveGuestByIdAsync(guestId);
                guestViewModel.Guest = guest;
            }
            catch (Exception ex)
            {
                guestViewModel.ErrorMessage = ex.Message;
            }
            finally
            {
                guestViewModel.IsLoading = false;
            }

            return guestViewModel;
        }

        public async ValueTask<GuestViewModel> ModifyGuestViewAsync(Guest guest)
        {
            var guestViewModel = new GuestViewModel { IsSubmitting = true };

            try
            {
                Guest modifiedGuest = await this.guestService.ModifyGuestAsync(guest);
                guestViewModel.Guest = modifiedGuest;
            }
            catch (Exception ex)
            {
                guestViewModel.ErrorMessage = ex.Message;
            }
            finally
            {
                guestViewModel.IsSubmitting = false;
            }

            return guestViewModel;
        }

        public async ValueTask<GuestViewModel> RemoveGuestViewByIdAsync(Guid guestId)
        {
            var guestViewModel = new GuestViewModel { IsSubmitting = true };

            try
            {
                Guest deletedGuest = await this.guestService.RemoveGuestByIdAsync(guestId);
                guestViewModel.Guest = deletedGuest;
            }
            catch (Exception ex)
            {
                guestViewModel.ErrorMessage = ex.Message;
            }
            finally
            {
                guestViewModel.IsSubmitting = false;
            }

            return guestViewModel;
        }
    }
}
