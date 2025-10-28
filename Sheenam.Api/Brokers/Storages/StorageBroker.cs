//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sheenam.Api.Models.Foundations.Guests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.configuration.GetConnectionString(name: "DatabaseConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

        public override void Dispose() { }

        public IQueryable<Guest> SelectAllGuests()
        {
            using var broker = new StorageBroker(this.configuration);

            return broker.Guests.AsQueryable();
        }
        public async ValueTask<Guest> SelectGuestByIdAsync(Guid Id)
        {
            using var broker = new StorageBroker(this.configuration);

            return await broker.Guests.FindAsync(Id);
        }
    }
}
