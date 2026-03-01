//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.RentalContracts.Exceptions
{
    public class NotFoundRentalContractException : Xeption
    {
        public NotFoundRentalContractException(Guid rentalContractId)
            : base(message: $"Rental contract not found with id: {rentalContractId}.") { }
    }
}