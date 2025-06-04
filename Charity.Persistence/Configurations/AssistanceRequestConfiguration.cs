using Charity.Domain.Entities;
using Charity.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations
{
    public class AssistanceRequestConfiguration : IEntityTypeConfiguration<AssistanceRequest>
    {
        public void Configure(EntityTypeBuilder<AssistanceRequest> builder)
        {
            builder.HasKey(ar => ar.Id);

            builder.Property(ar => ar.RequestDetails)
                .IsRequired(false);

            builder.Property(ar => ar.RequestStatus)
                .IsRequired()
                .HasConversion(ar => ar.ToString(),
                ar => Enum.Parse<RequestStatus>(ar));

            builder.HasIndex(ar => ar.BeneficiaryId)
                .HasDatabaseName("IX_AssistanceRequest_BeneficiaryId");

            builder.HasIndex(ar => ar.RequestStatus)
                .HasDatabaseName("IX_AssistanceRequest_RequestStatus");

            builder.HasOne(ar => ar.Beneficiary)
                .WithMany(u => u.AssistanceRequests)
                .HasForeignKey(ar => ar.BeneficiaryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("AssistanceRequests");
        }
    }
}
