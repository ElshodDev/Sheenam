//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.RentalContracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<RentalContract> RentalContracts { get; set; }

        public async ValueTask<RentalContract> InsertRentalContractAsync(RentalContract rentalContract) =>
           await InsertAsync(rentalContract);

        public IQueryable<RentalContract> SelectAllRentalContracts() =>
            SelectAll<RentalContract>();

        public async ValueTask<RentalContract> SelectRentalContractByIdAsync(Guid rentalContractId) =>
            await SelectAsync<RentalContract>(rentalContractId);

        public async ValueTask<RentalContract> UpdateRentalContractAsync(RentalContract rentalContract) =>
                         await UpdateAsync(rentalContract);

        public async ValueTask<RentalContract> DeleteRentalContractAsync(RentalContract rentalContract) =>
                            await DeleteAsync(rentalContract);
    }
}
