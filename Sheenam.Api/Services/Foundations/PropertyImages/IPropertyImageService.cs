//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.PropertyImages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.PropertyImages
{
    public interface IPropertyImageService
    {
        ValueTask<PropertyImage> AddPropertyImageAsync(PropertyImage propertyImage);
        IQueryable<PropertyImage> RetrieveAllPropertyImages();
        IQueryable<PropertyImage> RetrievePropertyImagesByPropertyId(Guid propertyId);
        ValueTask<PropertyImage> RetrievePropertyImageByIdAsync(Guid propertyImageId);
        ValueTask<PropertyImage> ModifyPropertyImageAsync(PropertyImage propertyImage);
        ValueTask<PropertyImage> RemovePropertyImageByIdAsync(Guid propertyImageId);
    }
}
