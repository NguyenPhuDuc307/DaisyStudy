using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class NotifycationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifycations");

            builder.HasKey(x => x.Notification_ID);

            builder.Property(x => x.Notification_ID).UseIdentityColumn();
            builder.Property(x => x.Class_ID).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.DatetimeCreated).IsRequired();

            builder.HasOne(x => x.Class).WithMany(x => x.Notifications).HasForeignKey(x => x.Class_ID);
        }
    }
}

