//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.RentalContracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<RentalContract> InsertRentalContractAsync(RentalContract rentalContract);
        IQueryable<RentalContract> SelectAllRentalContracts();
        ValueTask<RentalContract> SelectRentalContractByIdAsync(Guid rentalContractId);
        ValueTask<RentalContract> UpdateRentalContractAsync(RentalContract rentalContract);
        ValueTask<RentalContract> DeleteRentalContractAsync(RentalContract rentalContract);
    }
}
