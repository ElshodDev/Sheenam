//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.PropertyImages;
using Sheenam.Api.Models.Foundations.PropertyImages.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.PropertyImages
{
    public partial class PropertyImageService
    {
        private void ValidatePropertyImageOnAdd(PropertyImage propertyImage)
        {
            ValidatePropertyImageNotNull(propertyImage);

            Validate(
                (Rule: IsInvalid(propertyImage.Id), Parameter: nameof(PropertyImage.Id)),
                (Rule: IsInvalid(propertyImage.ImageUrl), Parameter: nameof(PropertyImage.ImageUrl)),
                (Rule: IsInvalid(propertyImage.CreatedDate), Parameter: nameof(PropertyImage.CreatedDate)));
        }

        private void ValidatePropertyImageOnModify(PropertyImage propertyImage)
        {
            ValidatePropertyImageNotNull(propertyImage);

            Validate(
                (Rule: IsInvalid(propertyImage.Id), Parameter: nameof(PropertyImage.Id)),
                (Rule: IsInvalid(propertyImage.ImageUrl), Parameter: nameof(PropertyImage.ImageUrl)));
        }

        private static void ValidatePropertyImageId(Guid propertyImageId) =>
            Validate((Rule: IsInvalid(propertyImageId), Parameter: nameof(PropertyImage.Id)));

        private static void ValidateStoragePropertyImage(
            PropertyImage maybePropertyImage, Guid propertyImageId)
        {
            if (maybePropertyImage is null)
                throw new NotFoundPropertyImageException(propertyImageId);
        }

        private static void ValidatePropertyImageNotNull(PropertyImage propertyImage)
        {
            if (propertyImage is null)
                throw new NullPropertyImageException();
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidPropertyImageException = new InvalidPropertyImageException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                    invalidPropertyImageException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
            }

            invalidPropertyImageException.ThrowIfContainsErrors();
        }
    }
}
