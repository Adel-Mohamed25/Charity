using Charity.Domain.Entities.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations.IdentityConfigurations
{
    public class JwtTokenConfiguration : IEntityTypeConfiguration<JwtToken>
    {
        public void Configure(EntityTypeBuilder<JwtToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");




            builder.HasOne(rf => rf.User)
                .WithMany(u => u.JwtTokens)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.ToTable("JwtTokens");
        }
    }
}
