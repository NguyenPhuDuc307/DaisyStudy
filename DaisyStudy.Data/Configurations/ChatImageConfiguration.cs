using System;
using DaisyStudy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaisyStudy.Data.Configurations
{
    public class ChatImageConfiguration : IEntityTypeConfiguration<ChatImage>
    {
        public void Configure(EntityTypeBuilder<ChatImage> builder)
        {
            builder.ToTable("ChatImages");

            builder.HasKey(x => x.ImageID);

            builder.Property(x => x.ChatID).IsRequired();
            builder.Property(x => x.ImageID).UseIdentityColumn();
            builder.Property(x => x.ImagePath).HasMaxLength(200).IsRequired();

            builder.HasOne(x => x.Chat).WithMany(x => x.ChatImages).HasForeignKey(x => x.ChatID);
        }
    }
}

