//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.RentalContracts;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string RentalContractsRelativeUrl = "api/rentalcontracts";

        public async ValueTask<RentalContract> PostRentalContractAsync(RentalContract rentalContract) =>
            await PostAsync(RentalContractsRelativeUrl, rentalContract);

        public async ValueTask<List<RentalContract>> GetAllRentalContractsAsync() =>
            await GetAsync<List<RentalContract>>(RentalContractsRelativeUrl);

        public async ValueTask<RentalContract> GetRentalContractByIdAsync(Guid rentalContractId) =>
            await GetAsync<RentalContract>($"{RentalContractsRelativeUrl}/{rentalContractId}");

        public async ValueTask<RentalContract> PutRentalContractAsync(RentalContract rentalContract) =>
            await PutAsync($"{RentalContractsRelativeUrl}/{rentalContract.Id}", rentalContract);

        public async ValueTask<RentalContract> DeleteRentalContractByIdAsync(Guid rentalContractId) =>
            await DeleteAsync<RentalContract>($"{RentalContractsRelativeUrl}/{rentalContractId}");
    }
}