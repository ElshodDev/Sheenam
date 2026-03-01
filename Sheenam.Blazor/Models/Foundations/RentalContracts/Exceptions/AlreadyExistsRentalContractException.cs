//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.RentalContracts.Exceptions
{
    public class AlreadyExistsRentalContractException : Xeption
    {
        public AlreadyExistsRentalContractException(Xeption innerException)
            : base(message: "Rental contract with the same id already exists.", innerException) { }
    }
}