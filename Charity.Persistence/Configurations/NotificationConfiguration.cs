using Charity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.ReceiverId)
                .IsRequired(false);


            builder.Property(n => n.Message)
                .IsRequired(false);

            builder.HasIndex(n => n.ReceiverId)
                .HasDatabaseName("IX_Notification_ReceiverId");

            builder.HasIndex(n => n.SenderId)
                .HasDatabaseName("IX_Notification_SenderId");

            builder.HasIndex(n => n.CreatedDate)
                .HasDatabaseName("IX_Notification_CreatedDate");


            builder.HasOne(n => n.Sender)
                .WithMany(n => n.SentNotifications)
                .HasForeignKey(n => n.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(n => n.Receiver)
                .WithMany(n => n.ReceivedNotifications)
                .HasForeignKey(n => n.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Notifications");
        }
    }
}
