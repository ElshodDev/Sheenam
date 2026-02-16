//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Sheenam.Blazor.Models.Foundations.Homes;
using Sheenam.Blazor.Services.Foundations.Homes;

namespace Sheenam.Blazor.Views.Components
{
    public partial class HomeFormComponent : ComponentBase
    {
        [Inject]
        public IHomeService HomeService { get; set; }

        [Parameter]
        public EventCallback OnSaveCallback { get; set; }

        public Home Home { get; set; } = new Home();
        public bool IsVisible { get; set; } = false;

        public void Open()
        {
            this.Home = new Home();
            this.IsVisible = true;
            StateHasChanged();
        }

        public void Close()
        {
            this.IsVisible = false;
            StateHasChanged();
        }

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var imageFile = e.File;
            if (imageFile != null)
            {
                var format = "image/png";
                var resizedImage = await imageFile.RequestImageFileAsync(format, 800, 600);

                using var stream = resizedImage.OpenReadStream(maxAllowedSize: 1024 * 1024 * 5);
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);

                var base64String = Convert.ToBase64String(ms.ToArray());
                this.Home.ImageUrls = $"data:{format};base64,{base64String}";

                StateHasChanged();
            }
        }

        private async Task OnSave()
        {
            try
            {
                bool isUpdate = this.Home.CreatedDate != default(DateTimeOffset);
                this.Home.UpdatedDate = DateTimeOffset.UtcNow;

                if (isUpdate)
                {
                    await this.HomeService.ModifyHomeAsync(this.Home);
                }
                else
                {
                    this.Home.Id = Guid.NewGuid();
                    this.Home.CreatedDate = this.Home.UpdatedDate;

                    if (this.Home.HostId == Guid.Empty)
                        this.Home.HostId = Guid.NewGuid();

                    await this.HomeService.AddHomeAsync(this.Home);
                }

                this.Close();
                await OnSaveCallback.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}