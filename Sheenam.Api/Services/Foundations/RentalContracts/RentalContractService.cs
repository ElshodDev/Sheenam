//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.DateTimes;
using Sheenam.Api.Brokers.Guids;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.RentalContracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.RentalContracts
{
    public partial class RentalContractService : IRentalContractService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly IGuidBroker guidBroker;

        public RentalContractService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker,
            IGuidBroker guidBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.guidBroker = guidBroker;
        }

        public ValueTask<RentalContract> AddRentalContractAsync(RentalContract rentalContract) =>
            TryCatch(async () =>
            {
                ValidateRentalContractOnAdd(rentalContract);

                rentalContract.Id = this.guidBroker.GetGuid();
                DateTimeOffset now = this.dateTimeBroker.GetCurrentDateTimeOffset();

                rentalContract.CreatedDate = now;
                rentalContract.UpdatedDate = now;
                rentalContract.SignedDate = now;

                rentalContract.Status = ContractStatus.Active;

                return await this.storageBroker.InsertRentalContractAsync(rentalContract);
            });

        public IQueryable<RentalContract> RetrieveAllRentalContracts() =>
            TryCatch(() => this.storageBroker.SelectAllRentalContracts());

        public ValueTask<RentalContract> RetrieveRentalContractByIdAsync(Guid rentalContractId) =>
            TryCatch(async () =>
            {
                ValidateRentalContractId(rentalContractId);

                RentalContract maybeRentalContract =
                    await this.storageBroker.SelectRentalContractByIdAsync(rentalContractId);

                ValidateStorageRentalContract(maybeRentalContract, rentalContractId);

                return maybeRentalContract;
            });

        public ValueTask<RentalContract> ModifyRentalContractAsync(RentalContract rentalContract) =>
            TryCatch(async () =>
            {
                ValidateRentalContractOnModify(rentalContract);

                RentalContract trackedRentalContract =
                    await this.storageBroker.SelectRentalContractByIdAsync(rentalContract.Id);

                ValidateStorageRentalContract(trackedRentalContract, rentalContract.Id);

                rentalContract.UpdatedDate = this.dateTimeBroker.GetCurrentDateTimeOffset();

                return await this.storageBroker.UpdateRentalContractAsync(rentalContract);
            });

        public ValueTask<RentalContract> RemoveRentalContractByIdAsync(Guid rentalContractId) =>
            TryCatch(async () =>
            {
                ValidateRentalContractId(rentalContractId);

                RentalContract maybeRentalContract =
                    await this.storageBroker.SelectRentalContractByIdAsync(rentalContractId);

                ValidateStorageRentalContract(maybeRentalContract, rentalContractId);

                return await this.storageBroker.DeleteRentalContractAsync(maybeRentalContract);
            });
    }
}