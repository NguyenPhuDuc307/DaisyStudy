using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(x => x.NotificationID);

            builder.Property(x => x.NotificationID).UseIdentityColumn();
            builder.Property(x => x.ClassID).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.DateTimeCreated).IsRequired();

            builder.HasOne(x => x.Class).WithMany(x => x.Notifications).HasForeignKey(x => x.ClassID);
        }
    }
}

