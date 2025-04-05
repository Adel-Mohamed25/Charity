using Charity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations
{
    public class UserVolunteerActivityConfiguration : IEntityTypeConfiguration<UserVolunteerActivity>
    {
        public void Configure(EntityTypeBuilder<UserVolunteerActivity> builder)
        {
            builder.HasKey(uv => new { uv.UserId, uv.VolunteerActivityId });

            builder.HasOne(uv => uv.User)
                .WithMany(u => u.VolunteerActivities)
                .HasForeignKey(uv => uv.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uv => uv.VolunteerActivity)
                .WithMany(a => a.Volunteers)
                .HasForeignKey(uv => uv.VolunteerActivityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("UserVolunteerActivities");


        }
    }
}
