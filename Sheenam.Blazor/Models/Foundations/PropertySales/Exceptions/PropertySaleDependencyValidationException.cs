//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.PropertySales.Exceptions
{
    public class PropertySaleDependencyValidationException : Xeption
    {
        public PropertySaleDependencyValidationException(Xeption innerException)
            : base(message: "Property sale dependency validation error occurred.", innerException) { }
    }
}