//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.RentalContracts.Exceptions
{
    public class AlreadyExistsRentalContractException : Xeption
    {
        public AlreadyExistsRentalContractException(Exception innerException)
            : base(message: "Rental contract already exists.", innerException)
        { }
    }
}