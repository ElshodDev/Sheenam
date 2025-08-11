//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To use  Comfort and Peace
//===================================================

using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.configuration.GetConnectionString(name: "DatabaseConnection");
       
        optionsBuilder.UseNpgsql(connectionString);
        }

        public override void Dispose() { }
    }
}
