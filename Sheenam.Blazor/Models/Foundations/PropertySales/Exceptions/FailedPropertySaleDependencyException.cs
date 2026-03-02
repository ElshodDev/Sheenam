//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.PropertySales.Exceptions
{
    public class FailedPropertySaleDependencyException : Xeption
    {
        public FailedPropertySaleDependencyException(Exception innerException)
            : base(message: "Failed property sale dependency error occurred.", innerException) { }
    }
}