//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Properties;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Property> InsertPropertyAsync(Property property);
        IQueryable<Property> SelectAllProperties();
        ValueTask<Property> SelectPropertyByIdAsync(Guid propertyId);
        ValueTask<Property> UpdatePropertyAsync(Property property);
        ValueTask<Property> DeletePropertyAsync(Property property);
    }
}