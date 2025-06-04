using Charity.Domain.Entities;
using Charity.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations
{
    public class VolunteerApplicationConfiguration : IEntityTypeConfiguration<VolunteerApplication>
    {
        public void Configure(EntityTypeBuilder<VolunteerApplication> builder)
        {
            builder.HasKey(va => va.Id);

            builder.Property(va => va.RequestDetails)
                .IsRequired(false);

            builder.Property(va => va.RequestStatus)
                .IsRequired()
                .HasConversion(va => va.ToString(),
                va => Enum.Parse<RequestStatus>(va));

            builder.HasIndex(va => va.RequestStatus)
                .HasDatabaseName("IX_VolunteerApplication_RequestStatus");

            builder.HasIndex(va => va.VolunteerActivityId)
                .HasDatabaseName("IX_VolunteerApplication_VolunteerActivityId");

            builder.HasIndex(va => va.ProjectId)
                .HasDatabaseName("IX_VolunteerApplication_ProjectId");

            builder.HasOne(va => va.Volunteer)
                .WithMany(u => u.VolunteerApplications)
                .HasForeignKey(va => va.VolunteerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(va => va.VolunteerActivity)
                .WithMany(va => va.VolunteerApplications)
                .HasForeignKey(va => va.VolunteerActivityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(va => va.CharityProject)
                .WithMany(va => va.VolunteerApplications)
                .HasForeignKey(va => va.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("VolunteerApplications");
        }
    }
}
