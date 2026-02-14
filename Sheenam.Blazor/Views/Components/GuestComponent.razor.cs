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
        [Inject]
        public IGuestService GuestService { get; set; }

        public IEnumerable<Guest> Guests { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.Guests = await this.GuestService.RetrieveAllGuestsAsync();
        }
    }
}