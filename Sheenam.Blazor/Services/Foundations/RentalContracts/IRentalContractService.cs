//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.RentalContracts;

namespace Sheenam.Blazor.Services.Foundations.RentalContracts
{
    public interface IRentalContractService
    {
        ValueTask<RentalContract> AddRentalContractAsync(RentalContract rentalContract);
        ValueTask<IQueryable<RentalContract>> RetrieveAllRentalContractsAsync();
        ValueTask<RentalContract> RetrieveRentalContractByIdAsync(Guid rentalContractId);
        ValueTask<RentalContract> ModifyRentalContractAsync(RentalContract rentalContract);
        ValueTask<RentalContract> RemoveRentalContractByIdAsync(Guid rentalContractId);
    }
}