using Charity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations
{
    internal class ProjectVolunteerConfiguration : IEntityTypeConfiguration<ProjectVolunteer>
    {
        public void Configure(EntityTypeBuilder<ProjectVolunteer> builder)
        {
            builder.HasKey(pv => new { pv.ProjectId, pv.VolunteerId });

            builder.HasIndex(pv => pv.ProjectId)
                .HasDatabaseName("IX_ProjectVolunteer_ProjectId");

            builder.HasIndex(pv => pv.VolunteerId)
                .HasDatabaseName("IX_ProjectVolunteer_VolunteerId");


            builder.HasOne(pv => pv.Volunteer)
                .WithMany(u => u.ProjectVolunteers)
                .HasForeignKey(pv => pv.VolunteerId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(pv => pv.Project)
                .WithMany(p => p.ProjectVolunteers)
                .HasForeignKey(pv => pv.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.ToTable("ProjectVolunteers");
        }
    }
}
