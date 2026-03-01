//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.RentalContracts.Exceptions
{
    public class FailedRentalContractDependencyException : Xeption
    {
        public FailedRentalContractDependencyException(Exception innerException)
            : base(message: "Failed rental contract dependency error occurred.", innerException) { }
    }
}