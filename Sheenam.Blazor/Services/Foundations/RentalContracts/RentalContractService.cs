//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.RentalContracts;

namespace Sheenam.Blazor.Services.Foundations.RentalContracts
{
    public partial class RentalContractService : IRentalContractService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public RentalContractService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<RentalContract> AddRentalContractAsync(RentalContract rentalContract) =>
            await TryCatch(async () =>
            {
                ValidateRentalContractOnAdd(rentalContract);
                return await this.apiBroker.PostRentalContractAsync(rentalContract);
            });

        public async ValueTask<IQueryable<RentalContract>> RetrieveAllRentalContractsAsync() =>
            await TryCatch(async () =>
            {
                var rentalContracts = await this.apiBroker.GetAllRentalContractsAsync();
                return rentalContracts.AsQueryable();
            });

        public async ValueTask<RentalContract> RetrieveRentalContractByIdAsync(Guid rentalContractId) =>
            await TryCatch(async () =>
            {
                ValidateRentalContractId(rentalContractId);
                return await this.apiBroker.GetRentalContractByIdAsync(rentalContractId);
            });

        public async ValueTask<RentalContract> ModifyRentalContractAsync(RentalContract rentalContract) =>
            await TryCatch(async () =>
            {
                ValidateRentalContractOnModify(rentalContract);
                return await this.apiBroker.PutRentalContractAsync(rentalContract);
            });

        public async ValueTask<RentalContract> RemoveRentalContractByIdAsync(Guid rentalContractId) =>
            await TryCatch(async () =>
            {
                ValidateRentalContractId(rentalContractId);
                return await this.apiBroker.DeleteRentalContractByIdAsync(rentalContractId);
            });
    }
}