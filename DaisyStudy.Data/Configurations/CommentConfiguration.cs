using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class CommentConfiguaration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(x => new { x.Notification_ID });

            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Likes).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.Dislikes).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.DatetimeCreated).IsRequired();

            builder.HasOne(x => x.Notification).WithMany(x => x.Comments).HasForeignKey(x => x.Notification_ID);
        }
    }
}

