//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.RentalContracts.Exceptions
{
    public class InvalidRentalContractException : Xeption
    {
        public InvalidRentalContractException()
            : base(message: "Rental contract is invalid.")
        { }
    }
}