//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.RentalContracts.Exceptions
{
    public class NullRentalContractException : Xeption
    {
        public NullRentalContractException()
            : base(message: "Rental contract is null.")
        { }
    }
}