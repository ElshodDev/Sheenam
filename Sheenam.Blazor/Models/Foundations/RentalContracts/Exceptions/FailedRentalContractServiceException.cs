//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.RentalContracts.Exceptions
{
    public class FailedRentalContractServiceException : Xeption
    {
        public FailedRentalContractServiceException(Exception innerException)
            : base(message: "Failed rental contract service error occurred.", innerException) { }
    }
}