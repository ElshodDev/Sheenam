//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.RentalContracts.Exceptions
{
    public class RentalContractValidationException : Xeption
    {
        public RentalContractValidationException(Xeption innerException)
            : base(message: "Rental contract validation error occurred.", innerException) { }
    }
}