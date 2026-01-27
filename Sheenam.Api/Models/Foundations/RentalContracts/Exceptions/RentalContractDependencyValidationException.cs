//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.RentalContracts.Exceptions
{
    public class RentalContractDependencyValidationException : Xeption
    {
        public RentalContractDependencyValidationException(Xeption innerException)
            : base(message: "Rental contract dependency validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}