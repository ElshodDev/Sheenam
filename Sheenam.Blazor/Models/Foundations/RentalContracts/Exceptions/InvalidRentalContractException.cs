//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.RentalContracts.Exceptions
{
    public class InvalidRentalContractException : Xeption
    {
        public InvalidRentalContractException()
            : base(message: "Invalid rental contract, fix errors and try again.") { }
    }
}