using Charity.Domain.Entities.IdentityEntities;
using Charity.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations.IdentityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);



            builder.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Address)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(u => u.PhoneNumber)
                .IsRequired();

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.Image)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(u => u.DateOfBirth)
                .IsRequired(false);

            builder.Property(u => u.Gender)
                .HasMaxLength(6)
                .IsRequired()
                .HasConversion(u => u.ToString(),
                u => Enum.Parse<GenderType>(u));



            builder.ToTable("Users");
        }
    }
}
