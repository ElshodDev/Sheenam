//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.RentalContracts.Exceptions
{
    public class NotFoundRentalContractException : Xeption
    {
        public NotFoundRentalContractException(Guid rentalContractId)
            : base(message: $"Couldn't find rental contract with id: {rentalContractId}.")
        { }
    }
}