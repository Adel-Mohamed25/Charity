using Charity.Domain.Entities;
using Charity.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations
{
    public class InKindDonationConfiguration : IEntityTypeConfiguration<InKindDonation>
    {
        public void Configure(EntityTypeBuilder<InKindDonation> builder)
        {
            builder.HasKey(ikd => ikd.Id);


            builder.Property(ikd => ikd.Name)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(ikd => ikd.Description)
                .IsRequired();

            builder.Property(ikd => ikd.ItemType)
                .IsRequired()
                .HasConversion(ikd => ikd.ToString(),
                ikd => Enum.Parse<DonationItemType>(ikd));

            builder.Property(ikd => ikd.DonationStatus)
                .IsRequired()
                .HasConversion(ikd => ikd.ToString(),
                ikd => Enum.Parse<DonationStatus>(ikd));


            builder.Property(ikd => ikd.DonorId)
                .IsRequired();

            builder.HasOne(ikd => ikd.Donor)
                .WithMany(u => u.InKindDonations)
                .HasForeignKey(ikd => ikd.DonorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ikd => ikd.Project)
                .WithMany(p => p.InKindDonations)
                .HasForeignKey(ikd => ikd.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(ikd => ikd.AssistanceRequests)
                .WithOne(ar => ar.InKindDonation)
                .HasForeignKey(ar => ar.InKindDonationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("InKindDonations");
        }
    }
}
