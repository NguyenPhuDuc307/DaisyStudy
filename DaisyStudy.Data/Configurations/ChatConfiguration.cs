using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class ChatConfiguaration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chats");

            builder.HasKey(x => x.Chat_ID);

            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Likes).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.Dislikes).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.DatetimeCreated).IsRequired();

            builder.HasOne(x => x.Class).WithMany(x => x.Chats).HasForeignKey(x => x.Class_ID);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Chats).HasForeignKey(x => x.User_ID);
        }
    }
}

