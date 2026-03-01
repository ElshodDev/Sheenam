//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.RentalContracts.Exceptions
{
    public class RentalContractDependencyException : Xeption
    {
        public RentalContractDependencyException(Xeption innerException)
            : base(message: "Rental contract dependency error occurred.", innerException) { }
    }
}