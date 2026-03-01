//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.RentalContracts;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<RentalContract> PostRentalContractAsync(RentalContract rentalContract);
        ValueTask<List<RentalContract>> GetAllRentalContractsAsync();
        ValueTask<RentalContract> GetRentalContractByIdAsync(Guid rentalContractId);
        ValueTask<RentalContract> PutRentalContractAsync(RentalContract rentalContract);
        ValueTask<RentalContract> DeleteRentalContractByIdAsync(Guid rentalContractId);
    }
}