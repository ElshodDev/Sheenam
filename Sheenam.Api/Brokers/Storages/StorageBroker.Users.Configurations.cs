//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Users;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void ConfigureUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

                entity.Property(u => u.PhoneNumber)
                    .HasMaxLength(20);

                entity.Property(u => u.Role)
                    .IsRequired()
                    .HasConversion<int>();

                entity.Property(u => u.IsEmailVerified)
                    .IsRequired()
                    .HasDefaultValue(false);
            });
        }
    }
}