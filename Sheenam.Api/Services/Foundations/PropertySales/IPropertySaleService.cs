using Sheenam.Api.Models.Foundations.PropertySales;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.PropertySales
{
    public interface IPropertySaleService
    {
        ValueTask<PropertySale> AddPropertySaleAsync(PropertySale propertySale);
        IQueryable<PropertySale> RetrieveAllPropertySales();
        ValueTask<PropertySale> RetrievePropertySaleByIdAsync(Guid propertySaleId);
        ValueTask<PropertySale> ModifyPropertySaleAsync(PropertySale propertySale);
        ValueTask<PropertySale> RemovePropertySaleByIdAsync(Guid propertySaleId);

        ValueTask<PropertySale> ApprovePropertySaleAsync(Guid propertySaleId);
        ValueTask<PropertySale> RejectPropertySaleAsync(Guid propertySaleId, string rejectionReason = null);
        ValueTask<PropertySale> CancelPropertySaleAsync(Guid propertySaleId);
        IQueryable<PropertySale> RetrievePropertySalesByStatusAsync(PropertySaleStatus status);
    }
}