using Charity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charity.Persistence.Configurations
{
    public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder.HasKey(un => new { un.UserId, un.NotificationId });

            builder.HasOne(un => un.User)
            .WithMany(u => u.ReceivedNotifications)
            .HasForeignKey(un => un.UserId)
            .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(un => un.Notification)
                .WithMany(u => u.UserNotifications)
                .HasForeignKey(un => un.NotificationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("UserNotifications");
        }
    }
}
