//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.PropertyViews;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.PropertyViews
{
    public interface IPropertyViewService
    {
        ValueTask<PropertyView> AddPropertyViewAsync(PropertyView propertyView);
        IQueryable<PropertyView> RetrieveAllPropertyViews();
        IQueryable<PropertyView> RetrievePropertyViewsByPropertyId(Guid propertyId);
        ValueTask<PropertyView> RetrievePropertyViewByIdAsync(Guid propertyViewId);
        ValueTask<PropertyView> RemovePropertyViewByIdAsync(Guid propertyViewId);
    }
}
