//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.PropertyViews;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<PropertyView> InsertPropertyViewAsync(PropertyView propertyView);
        IQueryable<PropertyView> SelectAllPropertyViews();
        ValueTask<PropertyView> SelectPropertyViewByIdAsync(Guid propertyViewId);
        ValueTask<PropertyView> DeletePropertyViewAsync(PropertyView propertyView);
    }
}
