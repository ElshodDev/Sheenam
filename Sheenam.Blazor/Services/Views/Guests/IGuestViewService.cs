//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Models.ViewModels;

namespace Sheenam.Blazor.Services.Views.Guests
{
    public interface IGuestViewService
    {
        ValueTask<GuestViewModel> AddGuestViewAsync(Guest guest);
        ValueTask<List<GuestViewModel>> RetrieveAllGuestViewsAsync();
        ValueTask<GuestViewModel> RetrieveGuestViewByIdAsync(Guid guestId);
        ValueTask<GuestViewModel> ModifyGuestViewAsync(Guest guest);
        ValueTask<GuestViewModel> RemoveGuestViewByIdAsync(Guid guestId);
    }
}
