using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chats");

            builder.HasKey(x => x.ChatID);

            builder.Property(x => x.ClassID).IsRequired();
            builder.Property(x => x.UserID).IsRequired();
            builder.Property(x => x.Likes).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.Dislikes).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.DateTimeCreated).IsRequired();

            builder.HasOne(x => x.Class).WithMany(x => x.Chats).HasForeignKey(x => x.ClassID);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Chats).HasForeignKey(x => x.UserID);
        }
    }
}

