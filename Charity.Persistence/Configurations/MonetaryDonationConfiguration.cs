using Charity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations
{
    public class MonetaryDonationConfiguration : IEntityTypeConfiguration<MonetaryDonation>
    {
        public void Configure(EntityTypeBuilder<MonetaryDonation> builder)
        {
            builder.HasKey(md => md.Id);

            builder.Property(md => md.Amount)
                .IsRequired()
                .HasPrecision(10, 4);

            builder.HasIndex(md => md.DonorId)
                .HasDatabaseName("IX_MonetaryDonation_DonorId");


            builder.HasOne(md => md.Donor)
                .WithMany(u => u.MonetaryDonations)
                .HasForeignKey(md => md.DonorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(md => md.Project)
                .WithMany(p => p.MonetaryDonations)
                .HasForeignKey(md => md.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("MonetaryDonations");
        }
    }
}
