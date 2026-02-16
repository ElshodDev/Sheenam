//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.RentalContracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.RentalContracts
{
    public interface IRentalContractService
    {
        ValueTask<RentalContract> AddRentalContractAsync(RentalContract rentalContract);
        IQueryable<RentalContract> RetrieveAllRentalContracts();
        ValueTask<RentalContract> RetrieveRentalContractByIdAsync(Guid rentalContractId);
        ValueTask<RentalContract> ModifyRentalContractAsync(RentalContract rentalContract);
        ValueTask<RentalContract> RemoveRentalContractByIdAsync(Guid rentalContractId);
    }
}
