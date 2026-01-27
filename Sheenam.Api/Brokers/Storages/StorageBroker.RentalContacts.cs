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

        public async ValueTask<RentalContract> InsertRentalContractAsync(RentalContract rentalContract)
        {
            var rentalContractEntityEntry = await this.RentalContracts.AddAsync(rentalContract);
            await this.SaveChangesAsync();
            return rentalContractEntityEntry.Entity;
        }

        public IQueryable<RentalContract> SelectAllRentalContracts() =>
           this.RentalContracts.AsQueryable();

        public async ValueTask<RentalContract> SelectRentalContractByIdAsync(Guid rentalContractId) =>
            await this.RentalContracts.AsNoTracking().FirstOrDefaultAsync(rc => rc.Id == rentalContractId);

        public async ValueTask<RentalContract> UpdateRentalContractAsync(RentalContract rentalContract)
        {
            var rentalContractEntityEntry =
                this.RentalContracts.Update(rentalContract);
            await this.SaveChangesAsync();
            return rentalContractEntityEntry.Entity;
        }

        public async ValueTask<RentalContract> DeleteRentalContractAsync(RentalContract rentalContract)
        {
            var rentalContractEntityEntry =
                this.RentalContracts.Remove(rentalContract);
            await this.SaveChangesAsync();
            return rentalContractEntityEntry.Entity;
        }

        public async ValueTask<RentalContract> SelectRentalContractByHomeIdAsync(Guid homeId) =>
            await this.RentalContracts.AsNoTracking().FirstOrDefaultAsync(rc => rc.HomeId == homeId);
    }
}
