using Charity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations
{
    internal class VolunteerActivityConfiguration : IEntityTypeConfiguration<VolunteerActivity>
    {
        public void Configure(EntityTypeBuilder<VolunteerActivity> builder)
        {
            builder.HasKey(va => va.Id);

            builder.Property(va => va.ActivityDescription)
                .IsRequired();

            builder.HasOne(va => va.Organizer)
                .WithMany(u => u.OrganizedVolunteerActivities)
                .HasForeignKey(va => va.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("VolunteerActivities");
        }
    }
}
