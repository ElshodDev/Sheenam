//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.RentalContracts;
using Sheenam.Api.Models.Orchestrations.HostDashboards;
using Sheenam.Api.Services.Foundations.Homes;
using Sheenam.Api.Services.Foundations.RentalContacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Orchestrations.HostDashboards
{
    public class HostDashboardOrchestrationService : IHostDashboardOrchestrationService
    {
        private readonly IHomeService homeService;
        private readonly IRentalContractService rentalContractService;

        public HostDashboardOrchestrationService(
            IHomeService homeService,
            IRentalContractService rentalContractService)
        {
            this.homeService = homeService;
            this.rentalContractService = rentalContractService;
        }

        public async ValueTask<HostDashboard> RetrieveHostDashboardDetailsAsync(Guid hostId)
        {
            IQueryable<Home> allHomes = this.homeService.RetrieveAllHomes();
            List<Home> hostHomes = allHomes.Where(home => home.HostId == hostId).ToList();
            List<Guid> homeIds = hostHomes.Select(home => home.Id).ToList();

            IQueryable<RentalContract> allContracts =
                this.rentalContractService.RetrieveAllRentalContracts();

            // Faqat shu mezbonning uylariga tegishli shartnomalarni olish
            List<RentalContract> hostContracts = allContracts
                .Where(contract => homeIds.Contains(contract.HomeId)).ToList();

            // Pullarni MonthlyRent orqali hisoblash
            decimal totalEarnings = hostContracts.Sum(contract => contract.MonthlyRent);

            return new HostDashboard
            {
                HostId = hostId,
                Houses = hostHomes,
                TotalEarnings = totalEarnings
            };
        }
    }
}