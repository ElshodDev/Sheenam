//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertySales.Exceptions
{
    public class PropertySaleDependencyException : Xeption
    {
        public PropertySaleDependencyException(Xeption innerException)
            : base(message: "PropertySale dependency error occured, contact support",
                  innerException)
        { }
    }
}
