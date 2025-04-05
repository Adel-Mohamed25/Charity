using Charity.Domain.Entities.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations.IdentityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<CharityRole>
    {
        public void Configure(EntityTypeBuilder<CharityRole> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("CharityRoles");
        }
    }
}
