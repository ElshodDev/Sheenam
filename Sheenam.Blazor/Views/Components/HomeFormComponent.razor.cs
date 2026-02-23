//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Sheenam.Blazor.Models.Foundations.Homes;
using Sheenam.Blazor.Services.Foundations.Homes;
using Sheenam.Blazor.Services.Foundations.Hosts;

namespace Sheenam.Blazor.Views.Components
{
    public partial class HomeFormComponent : ComponentBase
    {
        [Inject]
        public IHomeService HomeService { get; set; }

        [Inject]
        public IHostService HostService { get; set; }

        [Parameter]
        public EventCallback OnSaveCallback { get; set; }

        public Home Home { get; set; } = new Home();
        public bool IsVisible { get; set; } = false;
        public string ErrorMessage { get; set; }

        public List<Sheenam.Blazor.Models.Foundations.Hosts.Host> Hosts { get; set; }
        public Guid SelectedHostId { get; set; }
        public void Open()
        {
            this.Home = new Home();
            this.IsVisible = true;
            this.ErrorMessage = null;
            this.SelectedHostId = Guid.Empty;
            StateHasChanged();
        }

        public void Close()
        {
            this.IsVisible = false;
            this.ErrorMessage = null;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.Hosts = (await this.HostService.RetrieveAllHostsAsync()).ToList();
            }
            catch
            {
                this.Hosts = new List<Sheenam.Blazor.Models.Foundations.Hosts.Host>();
            }
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
                this.ErrorMessage = null;
                bool isUpdate = this.Home.Id != Guid.Empty;
                this.Home.UpdatedDate = DateTimeOffset.UtcNow;

                if (isUpdate)
                {
                    await this.HomeService.ModifyHomeAsync(this.Home);
                }
                else
                {
                    this.Home.Id = Guid.NewGuid();
                    this.Home.CreatedDate = this.Home.UpdatedDate;

                    if (this.SelectedHostId == Guid.Empty)
                    {
                        this.ErrorMessage = "Uy qo'shish uchun mezbon (Host) tanlang.";
                        return;
                    }

                    this.Home.HostId = this.SelectedHostId;

                    await this.HomeService.AddHomeAsync(this.Home);
                }

                this.Close();
                await OnSaveCallback.InvokeAsync();
            }
            catch (Exception ex)
            {
                this.ErrorMessage = "Saqlashda xatolik yuz berdi.";
                Console.WriteLine($"Xato sodir bo'ldi: {ex.Message}");
            }
        }
    }
}