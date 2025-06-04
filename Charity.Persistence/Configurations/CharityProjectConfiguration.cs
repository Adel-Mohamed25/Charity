using Charity.Domain.Entities;
using Charity.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations
{
    public class CharityProjectConfiguration : IEntityTypeConfiguration<CharityProject>
    {
        public void Configure(EntityTypeBuilder<CharityProject> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);


            builder.Property(c => c.Description)
                .IsRequired();

            builder.Property(c => c.StartDate)
                .IsRequired();

            builder.Property(c => c.EndDate)
                .IsRequired(false);


            builder.Property(c => c.TargetAmount)
                .IsRequired(false)
                .HasPrecision(18, 2);

            builder.Property(c => c.ProjectStatus)
                .HasConversion(c => c.ToString(),
                c => Enum.Parse<ProjectStatus>(c))
                .IsRequired();

            builder.Property(c => c.ManagerId)
                .IsRequired();

            builder.HasIndex(c => c.Name)
                .HasDatabaseName("IX_CharityProject_Name")
                .IsUnique();


            builder.HasOne(cp => cp.Manager)
                .WithMany(u => u.ManagedProjects)
                .HasForeignKey(cp => cp.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("CharityProjects");
        }
    }
}
