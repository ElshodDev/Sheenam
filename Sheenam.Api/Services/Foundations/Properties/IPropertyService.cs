//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Properties;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Properties
{
    public interface IPropertyService
    {
        ValueTask<Property> AddPropertyAsync(Property property);
        IQueryable<Property> RetrieveAllProperties();
        ValueTask<Property> RetrievePropertyByIdAsync(Guid propertyId);
        ValueTask<Property> ModifyPropertyAsync(Property property);
        ValueTask<Property> RemovePropertyByIdAsync(Guid propertyId);
    }
}