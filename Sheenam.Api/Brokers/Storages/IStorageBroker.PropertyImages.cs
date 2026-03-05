//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.PropertyImages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<PropertyImage> InsertPropertyImageAsync(PropertyImage propertyImage);
        IQueryable<PropertyImage> SelectAllPropertyImages();
        ValueTask<PropertyImage> SelectPropertyImageByIdAsync(Guid propertyImageId);
        ValueTask<PropertyImage> UpdatePropertyImageAsync(PropertyImage propertyImage);
        ValueTask<PropertyImage> DeletePropertyImageAsync(PropertyImage propertyImage);
    }
}
