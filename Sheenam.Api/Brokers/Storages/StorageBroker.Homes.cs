//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.Homes;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Home> Homes { get; set; }

        public async ValueTask<Home> InsertHomeAsync(Home home)
        {
            using var broker=new StorageBroker(this.configuration);

            EntityEntry<Home> homeEntityEntry = 
                await broker.Homes.AddAsync(home);

            await broker.SaveChangesAsync();

            return homeEntityEntry.Entity;
        }

    }
}
