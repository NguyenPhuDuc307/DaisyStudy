using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class NotificationImageConfiguration : IEntityTypeConfiguration<NotificationImage>
    {
        public void Configure(EntityTypeBuilder<NotificationImage> builder)
        {
            builder.ToTable("NotificationImages");

            builder.HasKey(x => x.ImageID);

            builder.Property(x => x.NotificationID).IsRequired();
            builder.Property(x => x.ImageID).UseIdentityColumn();
            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired();

            builder.HasOne(x => x.Notification).WithMany(x => x.NotificationImages).HasForeignKey(x => x.NotificationID);
        }
    }
}

