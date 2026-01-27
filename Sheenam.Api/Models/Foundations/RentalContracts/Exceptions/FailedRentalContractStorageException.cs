//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.RentalContracts.Exceptions
{
    public class FailedRentalContractStorageException : Xeption
    {
        public FailedRentalContractStorageException(Exception innerException)
            : base(message: "Failed rental contract storage error occurred, contact support.",
                  innerException)
        { }
    }
}