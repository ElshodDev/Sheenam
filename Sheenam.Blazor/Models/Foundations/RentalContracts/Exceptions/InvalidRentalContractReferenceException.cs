//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.RentalContracts.Exceptions
{
    public class InvalidRentalContractReferenceException : Xeption
    {
        public InvalidRentalContractReferenceException(Exception innerException)
            : base(message: "Invalid rental contract reference error occurred.", innerException) { }
    }
}