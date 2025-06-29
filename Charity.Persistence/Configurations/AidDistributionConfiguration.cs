﻿using Charity.Domain.Entities;
using Charity.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations
{
    public class AidDistributionConfiguration : IEntityTypeConfiguration<AidDistribution>
    {
        public void Configure(EntityTypeBuilder<AidDistribution> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(a => a.Status)
                .HasConversion(a => a.ToString(),
                a => Enum.Parse<AidDistributionStatus>(a))
                .IsRequired();

            builder.HasIndex(a => a.Status)
                .HasDatabaseName("IX_AidDistribution_Status");

            builder.Property(md => md.Amount)
                .HasPrecision(10, 4);

            builder.HasOne(a => a.Beneficiary)
                .WithMany(u => u.ReceivedAids)
                .HasForeignKey(a => a.BeneficiaryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Volunteer)
                .WithMany(u => u.DistributedAids)
                .HasForeignKey(a => a.VolunteerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.MonetaryDonation)
                .WithMany(m => m.AidDistributions)
                .HasForeignKey(a => a.MonetaryDonationId)
                .OnDelete(DeleteBehavior.SetNull);


            builder.HasOne(a => a.InKindDonation)
                .WithMany(i => i.AidDistributions)
                .HasForeignKey(a => a.InKindDonationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("AidDistributions");
        }
    }
}
