//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.AspNetCore.Components;
using Sheenam.Blazor.Models.Foundations.PropertySales;
using Sheenam.Blazor.Services.Foundations.Hosts;
using Sheenam.Blazor.Services.Foundations.PropertySales;
using Host = Sheenam.Blazor.Models.Foundations.Hosts.Host;
namespace Sheenam.Blazor.Views.Components
{
    public partial class PropertySaleFormComponent : ComponentBase
    {
        [Parameter]
        public EventCallback OnPropertySaleAdded { get; set; }

        [Inject]
        public IPropertySaleService PropertySaleService { get; set; }

        [Inject]
        public IHostService HostService { get; set; }

        public PropertySale propertySale = new PropertySale();
        public string errorMessage { get; set; } = string.Empty;
        public IEnumerable<Host> Hosts { get; set; } = new List<Host>();
        public DateTime listedDate { get; set; } = DateTime.Today;

        protected override async Task OnInitializedAsync()
        {
            await LoadDropdownsAsync();
            ResetForm();
        }

        private async Task LoadDropdownsAsync()
        {
            try
            {
                this.Hosts = await this.HostService.RetrieveAllHostsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dropdowns: {ex.Message}");
            }
        }

        public void EditPropertySale(PropertySale propertySaleToEdit)
        {
            this.propertySale = new PropertySale
            {
                Id = propertySaleToEdit.Id,
                HostId = propertySaleToEdit.HostId,
                Address = propertySaleToEdit.Address,
                Description = propertySaleToEdit.Description,
                Type = propertySaleToEdit.Type,
                NumberOfBedrooms = propertySaleToEdit.NumberOfBedrooms,
                NumberOfBathrooms = propertySaleToEdit.NumberOfBathrooms,
                Area = propertySaleToEdit.Area,
                SalePrice = propertySaleToEdit.SalePrice,
                Status = propertySaleToEdit.Status,
                ListedDate = propertySaleToEdit.ListedDate,
                SoldDate = propertySaleToEdit.SoldDate,
                ImageUrls = propertySaleToEdit.ImageUrls,
                LegalDocuments = propertySaleToEdit.LegalDocuments,
                IsFeatured = propertySaleToEdit.IsFeatured,
                CreatedDate = propertySaleToEdit.CreatedDate,
                UpdatedDate = propertySaleToEdit.UpdatedDate
            };

            this.listedDate = propertySaleToEdit.ListedDate.DateTime;
            StateHasChanged();
        }

        private void ResetForm()
        {
            this.propertySale = new PropertySale { Id = Guid.Empty };
            this.listedDate = DateTime.Today;
            this.errorMessage = string.Empty;
        }

        private async Task HandleSubmit()
        {
            if (propertySale.HostId == Guid.Empty)
            {
                errorMessage = "Iltimos, mezbonni tanlang!";
                return;
            }

            if (string.IsNullOrWhiteSpace(propertySale.Address))
            {
                errorMessage = "Iltimos, manzilni kiriting!";
                return;
            }

            if (propertySale.SalePrice <= 0)
            {
                errorMessage = "Narx 0 dan katta bo'lishi kerak!";
                return;
            }

            try
            {
                errorMessage = string.Empty;
                propertySale.ListedDate = new DateTimeOffset(listedDate);

                if (this.propertySale.Id == Guid.Empty)
                {
                    this.propertySale.Id = Guid.NewGuid();
                    var now = DateTimeOffset.UtcNow;
                    this.propertySale.CreatedDate = now;
                    this.propertySale.UpdatedDate = now;
                    await this.PropertySaleService.AddPropertySaleAsync(this.propertySale);
                }
                else
                {
                    this.propertySale.UpdatedDate = DateTimeOffset.UtcNow;
                    await this.PropertySaleService.ModifyPropertySaleAsync(this.propertySale);
                }

                await OnPropertySaleAdded.InvokeAsync();
                ResetForm();
            }
            catch (Exception ex)
            {
                errorMessage = "Xatolik yuz berdi: " + ex.Message;
            }
        }
    }
}