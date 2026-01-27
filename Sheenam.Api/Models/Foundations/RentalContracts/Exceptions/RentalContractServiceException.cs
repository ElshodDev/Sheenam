//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.RentalContracts.Exceptions
{
    public class RentalContractServiceException : Xeption
    {
        public RentalContractServiceException(Xeption innerException)
            : base(message: "Rental contract service error occurred, contact support.",
                  innerException)
        { }
    }
}