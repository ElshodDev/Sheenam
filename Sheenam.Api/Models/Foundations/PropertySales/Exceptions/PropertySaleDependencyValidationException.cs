//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertySales.Exceptions
{
    public class PropertySaleDependencyValidationException : Xeption
    {
        public PropertySaleDependencyValidationException(Xeption innerException)
            : base(message: "PropertySale dependency validation error occured, fix the errors and try again",
                  innerException)
        { }
    }
}
